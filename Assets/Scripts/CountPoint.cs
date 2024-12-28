using UnityEngine;
using TMPro;

public class CountPoint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _PointsText;
    [SerializeField] private AudioSource _Ringsource;
    [SerializeField] private AudioClip _Ringclip;

    private PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Invisible"))
        {
            playerStatus.AddPoints(5);
            if (_PointsText != null)
            {
                _PointsText.text = playerStatus.GetCurrentScore().ToString();
            }
            if (_Ringsource != null && _Ringclip != null)
            {
                _Ringsource.PlayOneShot(_Ringclip);
            }
        }
    }
}
