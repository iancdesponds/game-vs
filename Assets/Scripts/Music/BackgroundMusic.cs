using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public float startTime = 4f; // Começar no 4s
    public float endTime = 151f; // Parar no 151s (2:31)

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.time = startTime;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            if (audioSource.time >= endTime)
            {
                audioSource.Stop();
            }
        }
    }
}
