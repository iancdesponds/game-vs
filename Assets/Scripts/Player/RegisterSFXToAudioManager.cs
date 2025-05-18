using UnityEngine;

public class RegisterSFXToAudioManager : MonoBehaviour
{
    private void Start()
    {
        AudioSource myAudio = GetComponent<AudioSource>();

        if (AudioManager.Instance != null && myAudio != null)
        {
            AudioManager.Instance.sfxSource = myAudio;

            // Apply saved volume from PlayerPrefs
            float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 1f);
            AudioManager.Instance.SetSFXVolume(savedSFX);
        }
    }
}
