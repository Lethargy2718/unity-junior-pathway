using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioSource musicSource;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        float vol = PlayerPrefs.GetFloat("Volume");
        musicSource.volume = vol;
        slider.value = vol;
    }
    public void SetVolume(float newPercentage)
    {
        musicSource.volume = newPercentage;
        PlayerPrefs.SetFloat("Volume", newPercentage);
        PlayerPrefs.Save();
    }
}
