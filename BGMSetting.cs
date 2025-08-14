using UnityEngine;
using UnityEngine.UI;

public class BGMSettings : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Set posisi slider sesuai volume yang tersimpan
        if (BGMManager.instance != null)
            volumeSlider.value = BGMManager.instance.GetVolume();

        volumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
    }

    public void UpdateVolume()
    {
        if (BGMManager.instance != null)
            BGMManager.instance.SetVolume(volumeSlider.value);
    }
}
