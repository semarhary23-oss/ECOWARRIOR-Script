using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;

    // Tombol kembali
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // Fungsi Reset Semua Level
    public void ResetProgress()
    {
        // Hapus semua PlayerPrefs yang berkaitan dengan level
        for (int i = 2; i <= 5; i++)
        {
            string key = "Level" + i + "Unlocked";
            PlayerPrefs.DeleteKey(key);
        }

        PlayerPrefs.SetInt("Level1Unlocked", 1); // pastikan Level 1 tetap terbuka
        PlayerPrefs.Save();

        Debug.Log("Semua level dikunci ulang, kecuali Level 1.");
    }
}
