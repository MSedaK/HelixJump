using UnityEngine;

public class BallBounce : MonoBehaviour
{
    [SerializeField] private float _bounceHeight = 5f;
    [SerializeField] private float _fallSpeed = 5f; // D��me h�z� kontrol�
    [SerializeField] private AudioSource jumpsource;
    [SerializeField] private AudioClip jumpclip;

    private Rigidbody rb;
    private bool isMoving = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // S�rekli d��me hareketi i�in
        rb.velocity = new Vector3(0, -_fallSpeed, 0);
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // Yatay h�z� s�f�rla (sadece dikey hareket olsun)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            rb.velocity = new Vector3(0, _bounceHeight, 0);
            if (jumpsource != null && jumpclip != null)
            {
                jumpsource.PlayOneShot(jumpclip);
            }
        }
        else if (other.gameObject.CompareTag("TrickyPlatform"))
        {
            // Don't bounce on tricky platforms
            PlayerStatus playerStatus = GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.GameOver();
            }

            TrickyPlatform trickyPlatform = other.gameObject.GetComponent<TrickyPlatform>();
            if (trickyPlatform != null)
            {
                trickyPlatform.TriggerPlatformFall();
            }
        }
    }

    public void StopMovement()
    {
        isMoving = false;
        rb.velocity = Vector3.zero;
    }
}