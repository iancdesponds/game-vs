using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Tooltip("Painel que apresenta as opções de pausa (Resume, LoadMainMenu, etc.)")]
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Start()
    {
        // Garante que o menu de pausa comece desativado
        pauseMenuUI.SetActive(false);
    }

    // Chame este método no OnClick do seu botão "Pause"
    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }
}
