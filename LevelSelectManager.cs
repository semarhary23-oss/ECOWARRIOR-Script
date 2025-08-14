using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public Button[] levelButtons; // Ukuran: 5, urut dari Level 1 ke 5

    void Start()
    {
        // Loop untuk mengatur interaktif level
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == 0) // Level 1 selalu aktif
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                // Level2Unlocked disimpan sebagai 1, Level3Unlocked dst
                string key = "Level" + (i + 1) + "Unlocked";
                int unlocked = PlayerPrefs.GetInt(key, 0);
                levelButtons[i].interactable = (unlocked == 1);
            }
        }
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene("Level" + levelNumber); // Pastikan scene-nya bernama "Level1", "Level2", dll
    }

public void GoToMainMenu()
{
    // Jika sudah di MainMenu, jangan reload
    if (SceneManager.GetActiveScene().name != "MainMenu")
    {
        SceneManager.LoadScene("MainMenu");
    }
}
}
