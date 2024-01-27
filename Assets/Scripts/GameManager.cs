using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float maxAlertLevel = 100f;
    public float alertDecreaseRate = 0.5f;
    public float timeToStartDecrease = 5f;
    
    private float currentAlertLevel = 0f;
    private float timeSinceLastDetected = 0f;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        score = 0;
        UpdateScoreText();
        //Maybe Fade In?
    }

    private void Update()
    {
        if (currentAlertLevel > 0 && timeSinceLastDetected >= timeToStartDecrease)
        {
            DecreaseAlertLevel(alertDecreaseRate * Time.deltaTime);
        }

        timeSinceLastDetected += Time.deltaTime;
    }

    public void IncreaseAlertLevel(float amount)
    {
        currentAlertLevel += amount;
        if (currentAlertLevel > maxAlertLevel)
        {
            currentAlertLevel = maxAlertLevel;
        }

        timeSinceLastDetected = 0f;
        CheckForGameOver();
    }

    public void DecreaseAlertLevel(float amount)
    {
        currentAlertLevel -= amount;
        if (currentAlertLevel < 0)
        {
            currentAlertLevel = 0;
        }
    }

    private void CheckForGameOver()
    {
        if (currentAlertLevel >= maxAlertLevel)
        {
            //GameOver
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void GameOver()
    {
        //Fade Out
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
