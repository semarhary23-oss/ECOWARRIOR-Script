using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip bgmMenu;
    public AudioClip bgmLevel;
    public AudioClip clickClip;
    public AudioClip benarClip;
    public AudioClip salahClip;
    public AudioClip ambilClip;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // agar tidak hancur antar scene

            // Pastikan SFX aktif
    if (sfxSource != null && !sfxSource.gameObject.activeSelf)
    {
        sfxSource.gameObject.SetActive(true);
    }
    if (!sfxSource.enabled)
    {
        sfxSource.enabled = true;
    }

        LoadVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    // ------------------ BGM ------------------

    public void PlayBGMMenu()
    {
        if (bgmSource.clip != bgmMenu)
        {
            bgmSource.clip = bgmMenu;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlayBGMLevel()
    {
        if (bgmSource.clip != bgmLevel)
        {
            bgmSource.clip = bgmLevel;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // ------------------ SFX ------------------

    public void PlayClick() => PlaySFX(clickClip);
    public void PlayBenar() => PlaySFX(benarClip);
    public void PlaySalah() => PlaySFX(salahClip);
    public void PlayAmbil() => PlaySFX(ambilClip);

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
            sfxSource.PlayOneShot(clip);
    }

    // ------------------ Volume Control ------------------

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
