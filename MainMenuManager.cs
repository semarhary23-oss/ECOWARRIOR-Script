using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject settingsPanel;
    public GameObject profilePanel;

    public void OpenStartPanel()
    {
        startPanel.SetActive(true);
    }

    public void CloseStartPanel()
    {
        startPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }


    public void ShowProfilePanel()
    {
        Debug.Log("ShowProfilePanel terpanggil!");
        profilePanel.SetActive(true);
    }

    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
