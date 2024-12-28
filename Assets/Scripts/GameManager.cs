using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    private int totalScore = 0;
    private int currentLevelScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        currentLevelScore += points;
    }

    public int GetCurrentScore()
    {
        return totalScore + currentLevelScore;
    }

    public void CommitLevelScore()
    {
        totalScore += currentLevelScore;
        currentLevelScore = 0;
    }

    public void ResetCurrentLevelScore()
    {
        currentLevelScore = 0;
    }

    public void ResetAllScores()
    {
        totalScore = 0;
        currentLevelScore = 0;
    }
}
