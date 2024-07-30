using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // UI Elements (consider using a more descriptive name like "gameEndUI")
    public GameObject gameOverScreen;
    public TextMeshProUGUI totalSoulsText;

    // Components and Game Objects
    private EnemySpawner enemySpawner;
    public AudioSource gameOverSound;

    // Buttons (made private since only used internally)
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    void Start()
    {
        // Cache references in Start for efficiency
        enemySpawner = FindObjectOfType<EnemySpawner>();

        // Deactivate game over UI and buttons on start
        gameOverScreen.SetActive(false);

        // Initialize text (consider moving this to a separate method for readability)
        totalSoulsText.text = "Total Souls Saved: 0";
    }

    public void GameOver()
    {
        if (gameOverSound != null)
        {
            gameOverSound.Play();
        }

        gameOverScreen.SetActive(true); // Show game over screen and buttons

        // Stop enemy spawning
        enemySpawner?.StopSpawning(); // Safe way to check if enemySpawner exists

        // Update final coin text
        totalSoulsText.text = "Total Souls Saved: " + SoulManager.instance.souls;

        // Play game over sound
        gameOverSound?.Play(); // Safe way to check if gameOverSound exists

        // Pause the game
        Time.timeScale = 0f;
    }

    // Restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload by index, more reliable
    }

    // Return to main menu
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Load by scene name
    }
}
