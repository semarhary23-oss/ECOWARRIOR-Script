using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Dropdown characterDropdown;
    public GameObject profilePanel;

    public Transform previewSpawnPoint; // tempat instansiasi
    public GameObject[] characterPrefabs; // karakter 3D prefab

    private GameObject currentPreview; // referensi karakter yang ditampilkan

    void Start()
    {
        nameInput.text = PlayerPrefs.GetString("PlayerName", "");
        characterDropdown.value = PlayerPrefs.GetInt("CharacterIndex", 0);
        ShowCharacterPreview(characterDropdown.value);

        characterDropdown.onValueChanged.AddListener(ShowCharacterPreview);
    }

    public void ShowProfilePanel()
    {
        profilePanel.SetActive(true);
    }

    public void CloseProfilePanel()
    {
        profilePanel.SetActive(false);
    }

    public void SaveProfile()
    {
        if (string.IsNullOrEmpty(nameInput.text))
        {
            Debug.LogWarning("Nama belum diisi.");
            return;
        }

        PlayerPrefs.SetString("PlayerName", nameInput.text);
        PlayerPrefs.SetInt("CharacterIndex", characterDropdown.value);
        PlayerPrefs.Save();

        profilePanel.SetActive(false);
    }

    void ShowCharacterPreview(int index)
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        if (index >= 0 && index < characterPrefabs.Length)
        {
            currentPreview = Instantiate(characterPrefabs[index], previewSpawnPoint.position, previewSpawnPoint.rotation);
            currentPreview.transform.localScale = Vector3.one * 1.2f; // sesuaikan ukuran
            currentPreview.GetComponent<Animator>().enabled = false; // opsional: matikan animasi preview
        }
    }
}
