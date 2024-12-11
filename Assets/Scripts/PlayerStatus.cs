using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Bir kez tanýmlandý
    public GameObject RestartMenu;
    public GameObject FinishMenu;
    private Rigidbody _rb;

    [SerializeField] private AudioSource _Trapsource;
    [SerializeField] private AudioClip _Trapclip;

    [SerializeField] private AudioSource _Winsource;
    [SerializeField] private AudioClip _Winclip;

    // Skor deðiþkeni
    public static int Score = 0;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            _rb.isKinematic = true;
            _Trapsource.PlayOneShot(_Trapclip);
            RestartMenu.SetActive(true);
        }
        else if (other.gameObject.CompareTag("FinishLine"))
        {
            _Winsource.PlayOneShot(_Winclip);
            FinishMenu.SetActive(true);
            _rb.isKinematic = true;

            // Skoru bir artýr ve UI'ya yansýt
            Score++;
            UpdateScoreText();
            Debug.Log("Skor: " + Score);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Score.ToString();
        }
    }
}

