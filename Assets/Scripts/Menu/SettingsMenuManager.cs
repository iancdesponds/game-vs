using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Load saved volume or default to 1.0
        float savedBGM = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 1f);

        bgmSlider.value = savedBGM;
        sfxSlider.value = savedSFX;

        // Apply volumes
        SetBGMVolume(savedBGM);
        SetSFXVolume(savedSFX);

        // Hook up listeners
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetBGMVolume(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetBGMVolume(value);

        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetSFXVolume(value);

        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}