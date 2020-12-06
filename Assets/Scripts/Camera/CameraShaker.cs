using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float MinJumpDistance = 1f;
    public float MaxJumpDistance = 2f;
    public int Jumps = 5;
    public float Delay = 0.1f;
    private bool _shaking = false;

    public void Shake()
    {
        if (_shaking)
            StopCoroutine(ShakeRoutine());
        StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        _shaking = true;
        for (int i = 0; i < Jumps; i++)
        {
            float x = Random.Range(MinJumpDistance, MaxJumpDistance) * RandomSign();
            float y = Random.Range(MinJumpDistance, MaxJumpDistance) * RandomSign();
            transform.localPosition = new Vector3(x, y, 0f);
            yield return new WaitForSeconds(Delay);
        }
        transform.localPosition = Vector3.zero;
        _shaking = false;
    }

    private int RandomSign()
    {
        return Random.value > 0.5f ? 1 : -1;
    }


}
