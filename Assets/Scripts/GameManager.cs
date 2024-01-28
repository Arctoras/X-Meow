using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // public float maxAlertLevel = 100f;
    // public float alertDecreaseRate = 0.5f;
    // public float timeToStartDecrease = 5f;

    public int totalBlocks = 5;
    private int clickedBlocks = 0;
    
    // private float currentAlertLevel = 0f;
    // private float timeSinceLastDetected = 0f;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject gameManager;
    public GameObject gameCanvas;

    public Image GameOverImage;
    public Sprite TimeUpSprite;
    public Sprite LoudSoundSprite;
    public TMP_Text GameOverScore;

    public GameObject PauseMenu;

    public bool isGameStarted = false;
    public bool isGameOver = false;
    
    void Awake()
    {
        GameOverImage.gameObject.SetActive(false);
        gameManager.GetComponent<Timer>().enabled = false;
        gameCanvas.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
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

    public void BlockClicked()
    {
        clickedBlocks++;
        if (clickedBlocks >= totalBlocks)
        {
            Invoke(nameof(StartGame),1f);
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        gameManager.GetComponent<Timer>().enabled = true;
        gameCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        // if (currentAlertLevel > 0 && timeSinceLastDetected >= timeToStartDecrease)
        // {
        //     DecreaseAlertLevel(alertDecreaseRate * Time.deltaTime);
        // }
        //
        // timeSinceLastDetected += Time.deltaTime;
    }

    // public void IncreaseAlertLevel(float amount)
    // {
    //     currentAlertLevel += amount;
    //     if (currentAlertLevel > maxAlertLevel)
    //     {
    //         currentAlertLevel = maxAlertLevel;
    //     }
    //
    //     timeSinceLastDetected = 0f;
    //     CheckForGameOver();
    // }
    //
    // public void DecreaseAlertLevel(float amount)
    // {
    //     currentAlertLevel -= amount;
    //     if (currentAlertLevel < 0)
    //     {
    //         currentAlertLevel = 0;
    //     }
    // }
    //
    // private void CheckForGameOver()
    // {
    //     if (currentAlertLevel >= maxAlertLevel)
    //     {
    //         //GameOver
    //     }
    // }

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

    public void GameOver(bool isTimeOut)
    {
        isGameOver = true;
        GameOverImage.gameObject.SetActive(true);
        GameOverScore.text = score.ToString();
        if (isTimeOut)
        {
            GameOverImage.sprite = TimeUpSprite;
        }
        else
        {
            GameOverImage.sprite = LoudSoundSprite;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        if (isGameStarted)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
