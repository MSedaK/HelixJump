using UnityEngine;

public class BallBounce : MonoBehaviour
{
    [SerializeField] private float _bounceHeight = 5f;
    [SerializeField] private float _fallSpeed = 5f; // Düþme hýzý kontrolü
    [SerializeField] private AudioSource jumpsource;
    [SerializeField] private AudioClip jumpclip;

    private Rigidbody rb;
    private bool isMoving = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Sürekli düþme hareketi için
        rb.velocity = new Vector3(0, -_fallSpeed, 0);
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // Yatay hýzý sýfýrla (sadece dikey hareket olsun)
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
            rb.velocity = new Vector3(0, _bounceHeight, 0);
            if (jumpsource != null && jumpclip != null)
            {
                jumpsource.PlayOneShot(jumpclip);
            }

            // Tricky platform'u tetikle
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