using UnityEngine;
using TMPro;  // Make sure TextMeshPro is imported
using UnityEngine.UI;  // For using UI elements like Button

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // Game speed parameters
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public Button retryButton;

    private Player player;
    private Spawner spawner;

    private float score;

    private void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if necessary
        }
        else
        {
            DestroyImmediate(gameObject); // Destroy duplicate GameManager
        }
    }

    private void OnDestroy()
    {
        // Clear the instance if this GameManager is being destroyed
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        // Add listener to retry button for restarting the game
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(NewGame);
        }

        // Load the high score
        UpdateHiscore();

        NewGame(); // Initialize game settings at the start
    }

    public void GameOver()
    {
        // Stop the game logic
        gameSpeed = 0f;

        // Disable player and spawner when the game is over
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        // Enable retry button and game over text
        if (retryButton != null) retryButton.gameObject.SetActive(true);
        if (gameOverText != null) gameOverText.gameObject.SetActive(true);

        UpdateHiscore();
    }

    private void Update()
    {
        // Gradually increase game speed over time
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;

        // Update score text display
        scoreText.text = Mathf.RoundToInt(score).ToString("D5");
    }

    // Reset game state for a new game
    public void NewGame()
    {
        // Destroy all obstacles when starting a new game
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0f; // Reset score to zero
        gameSpeed = initialGameSpeed; // Reset game speed to its initial value

        // Reactivate the player and spawner
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        // Hide retry button and game over text
        if (retryButton != null) retryButton.gameObject.SetActive(false);
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);

        // Reset score text
        scoreText.text = Mathf.RoundToInt(score).ToString("D5");
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0); // Fixed typo

        // Check if the current score is greater than the high score
        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore); // Added missing semicolon
        }

        // Update high score text display
        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
