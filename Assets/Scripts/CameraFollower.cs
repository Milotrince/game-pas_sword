using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform CameraTransform;
    public Transform TargetTransform;
    public float LerpValue = 0.008f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Vector3.Lerp(CameraTransform.position, TargetTransform.position, LerpValue);
        newPosition.z = CameraTransform.position.z;
        CameraTransform.position = newPosition;
    }
}
