using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    public GameObject RestartMenu;
    public GameObject FinishMenu;
    private Rigidbody _rb;
    private BallBounce _ballBounce;

    [SerializeField] private AudioSource _Trapsource;
    [SerializeField] private AudioClip _Trapclip;
    [SerializeField] private AudioSource _Winsource;
    [SerializeField] private AudioClip _Winclip;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _ballBounce = GetComponent<BallBounce>();
        InitializeScoreDisplay();
    }

    private void InitializeScoreDisplay()
    {
        if (scoreText != null)
        {
            // Level baþladýðýnda toplam skoru göster
            scoreText.text = "Score: " + GameManager.Instance.GetCurrentScore().ToString();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trap") || other.gameObject.CompareTag("TrickyPlatform"))
        {
            GameOver();
        }
        else if (other.gameObject.CompareTag("FinishLine"))
        {
            LevelComplete();
        }
    }

    public void GameOver()
    {
        _rb.isKinematic = true;
        _ballBounce.StopMovement();
        _Trapsource.PlayOneShot(_Trapclip);

        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + GameManager.Instance.GetCurrentScore().ToString();
        }
        RestartMenu.SetActive(true);
    }

    private void LevelComplete()
    {
        _rb.isKinematic = true;
        _ballBounce.StopMovement();
        _Winsource.PlayOneShot(_Winclip);

        // Önce mevcut level skorunu toplam skora ekle
        GameManager.Instance.CommitLevelScore();

        if (finalScoreText != null)
        {
            finalScoreText.text = "Total Score: " + GameManager.Instance.GetCurrentScore().ToString();
        }
        FinishMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        // Level baþarýsýz olduðunda tüm skorlarý sýfýrla
        GameManager.Instance.ResetAllScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        // Skoru sýfýrlamadan bir sonraki levele geç
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void AddPoints(int points)
    {
        GameManager.Instance.AddPoints(points);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.GetCurrentScore().ToString();
        }
    }

    public int GetCurrentScore()
    {
        return GameManager.Instance.GetCurrentScore();
    }
}

