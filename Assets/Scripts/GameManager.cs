using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    //public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalCoinText;
    public EnemySpawner enemySpawner;
    public AudioSource gameOverSound;

    // References to the buttons
    public Button restartButton;
    public Button mainMenuButton;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        //finalScoreText.gameObject.SetActive(false);
        finalCoinText.gameObject.SetActive(false);

        // Make sure the buttons are initially inactive
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);

        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        //finalScoreText.gameObject.SetActive(true);
        finalCoinText.gameObject.SetActive(true);

        // Activate the buttons
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);

        //int finalScore = 0;
        //finalScoreText.text = "Final Score: " + finalScore;

        // Get the total coins from CoinManager
        int totalCoins = CoinManager.instance.coins;
        finalCoinText.text = "Total Souls Saved: " + totalCoins;

        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }

        if (gameOverSound != null)
        {
            gameOverSound.Play();
        }

        // Pause the game
        Time.timeScale = 0f;
    }

    // Restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Return to main menu
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
