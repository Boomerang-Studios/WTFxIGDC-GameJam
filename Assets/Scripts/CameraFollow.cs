using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 leftEdge;
    public Vector3 rightEdge;

    [Range(0f, 1f)]
    public float followSpeed = 0.1f;

    private void FixedUpdate()
    {
        float playerXPos = target.position.x;
        playerXPos = Mathf.Clamp(playerXPos, leftEdge.x, rightEdge.x);
        float cameraXPos = Mathf.Lerp(transform.position.x, playerXPos, followSpeed);
        transform.position = new Vector3(cameraXPos, transform.position.y, transform.position.z);
    }
}