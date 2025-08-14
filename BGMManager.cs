using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // BGM tetap hidup antar scene
            audioSource = GetComponent<AudioSource>();

            // Load volume dari PlayerPrefs (jika ada)
            float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
            audioSource.volume = savedVolume;
        }
        else
        {
            Destroy(gameObject); // Hapus duplikat saat kembali ke MainMenu
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }
}
    