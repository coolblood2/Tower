using UnityEngine;
using TMPro;

public class SoulManager : MonoBehaviour // Renamed to SoulManager
{
    public static SoulManager instance;
    public int souls = 0;
    public TMP_Text soulText; // Reference to the TextMeshPro Text component

    private const string SOULS_KEY = "Souls"; // Changed key to "Souls"

    void Awake()
    {
        // (Singleton implementation remains the same)
    }

    void Start()
    {
        souls = PlayerPrefs.GetInt(SOULS_KEY, 0); // Load saved souls
        UpdateSoulDisplay(); // Renamed to UpdateSoulDisplay
    }

    public void AddSouls(int amount) // Renamed to AddSouls
    {
        souls += amount;
        //Debug.Log("Souls added: " + amount + ". Total souls: " + souls);
        UpdateSoulDisplay();
        PlayerPrefs.SetInt(SOULS_KEY, souls); // Save souls
    }

    public bool SpendSouls(int amount) // Renamed to SpendSouls
    {
        if (souls >= amount)
        {
            souls -= amount;
            UpdateSoulDisplay();
            PlayerPrefs.SetInt(SOULS_KEY, souls); // Save souls
            return true;
        }
        return false;
    }

    void UpdateSoulDisplay() // Renamed to UpdateSoulDisplay
    {
        if (soulText != null)
        {
            soulText.text = "Souls: " + souls; // Changed text
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(SOULS_KEY, souls); // Ensure souls are saved on quit
    }
}
