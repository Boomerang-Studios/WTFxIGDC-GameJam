using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform leftEdge;
    public Transform rightEdge;

    [Range(0f, 1f)]
    public float followSpeed = 0.1f;

    private void FixedUpdate()
    {
        float playerXPos = target.position.x;
        playerXPos = Mathf.Clamp(playerXPos, leftEdge.position.x, rightEdge.position.x);
        float cameraXPos = Mathf.Lerp(transform.position.x, playerXPos, followSpeed);
        transform.position = new Vector3(cameraXPos, transform.position.y, transform.position.z);
    }
}