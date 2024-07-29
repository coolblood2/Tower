using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainLevel()
    {
        SceneManager.LoadScene("MainLevel"); // Replace "MainLevel" with the exact name of your main game scene
    }
}
