using UnityEngine;
using TMPro;  // Required for TextMeshPro

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int coins = 0;
    public TMP_Text coinText;  // Reference to the TextMeshPro Text component

    private const string COINS_KEY = "Coins";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        coins = PlayerPrefs.GetInt(COINS_KEY, 0);  // Load saved coins or default to 0
        UpdateCoinDisplay();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        //Debug.Log("Coins added: " + amount + ". Total coins: " + coins);
        UpdateCoinDisplay();
        PlayerPrefs.SetInt(COINS_KEY, coins);  // Save coins
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateCoinDisplay();
            PlayerPrefs.SetInt(COINS_KEY, coins);  // Save coins
            return true;
        }
        return false;
    }

    void UpdateCoinDisplay()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coins;
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(COINS_KEY, coins);  // Ensure coins are saved on quit
    }
}
