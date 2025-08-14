using UnityEngine;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nameInputField;
    public TMP_Dropdown characterDropdown;
    public GameObject previewSpot; // Tempat menaruh karakter preview
    public GameObject simpanButton;

    private GameObject currentCharacterInstance;

    private string[] characterNames = { "MoonMan" };

    void Start()
    {
        // Isi dropdown
        characterDropdown.ClearOptions();
        characterDropdown.AddOptions(new System.Collections.Generic.List<string>(characterNames));

        // Pasang listener
        characterDropdown.onValueChanged.AddListener(OnCharacterSelected);

        // Tampilkan karakter default (pertama)
        OnCharacterSelected(0);
    }

    void OnCharacterSelected(int index)
    {
        // Hapus preview sebelumnya
        if (currentCharacterInstance != null)
        {
            Destroy(currentCharacterInstance);
        }

        string selectedCharacter = characterNames[index];

        // Load prefab dari Resources
        GameObject prefab = Resources.Load<GameObject>("Characters/" + selectedCharacter);
        if (prefab != null)
        {
            currentCharacterInstance = Instantiate(prefab, previewSpot.transform.position, Quaternion.identity);
            currentCharacterInstance.transform.SetParent(previewSpot.transform); // Agar tidak keluar dari posisi preview
            currentCharacterInstance.transform.localScale = Vector3.one; // Pastikan skala pas
        }
        else
        {
            Debug.LogError("Prefab " + selectedCharacter + " tidak ditemukan di Resources/Characters/");
        }
    }

    public void OnClickSimpan()
    {
        string nama = nameInputField.text;
        string karakter = characterNames[characterDropdown.value];

        PlayerPrefs.SetString("NamaPemain", nama);
        PlayerPrefs.SetString("KarakterPemain", karakter);
        PlayerPrefs.Save();

        Debug.Log("Data disimpan: " + nama + ", " + karakter);

        // Lanjut ke scene berikutnya kalau perlu:
        // SceneManager.LoadScene("Level1");
    }
}
