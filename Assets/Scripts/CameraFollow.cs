using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ball;
    public Vector3 offset = new Vector3(0, 10, -7); // Helix Jump tarzý kamera açýsý

    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private float minY = -100f; // Kameranýn en düþük Y pozisyonu

    void LateUpdate()
    {
        if (ball == null) return;

        // Sadece top bizden aþaðýdaysa takip et
        if (ball.position.y < transform.position.y)
        {
            Vector3 desiredPosition = new Vector3(
                transform.position.x,
                Mathf.Max(ball.position.y + offset.y, minY),
                transform.position.z
            );

            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                desiredPosition,
                smoothSpeed
            );

            transform.position = smoothedPosition;
        }
    }
}