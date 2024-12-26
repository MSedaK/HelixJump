using UnityEngine;
using DG.Tweening;

public class TrickyPlatform : MonoBehaviour
{
    private float _duration = 1.5f; // Daha h�zl� animasyon
    public Transform target;

    [SerializeField] private AudioSource _Tricksource;
    [SerializeField] private AudioClip _Trickclip;

    // Platform �zerindeki collision detection'� ball'a ta��yoruz
    public void TriggerPlatformFall()
    {
        transform.DOMoveY(target.position.y, _duration);
        if (_Tricksource != null && _Trickclip != null)
        {
            _Tricksource.PlayOneShot(_Trickclip);
        }
    }
}