using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform targetTransform;
    public float lerp = 0.008f;

    // Update is called once per frame
    void Update() {
        Vector3 newPosition = Vector3.Lerp(cameraTransform.position, targetTransform.position, lerp);
        newPosition.z = cameraTransform.position.z;
        cameraTransform.position = newPosition;
    }
}
