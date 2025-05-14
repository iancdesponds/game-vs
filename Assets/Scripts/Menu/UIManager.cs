using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("End Game UI")]
    public GameObject endGamePanel;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (endGamePanel != null)
            endGamePanel.SetActive(false);
    }

    public void ShowEndGame()
    {
        if (endGamePanel != null)
            endGamePanel.SetActive(true);
    }

    // Botão: Reiniciar o jogo
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    // Botão: Voltar ao menu
    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(0); // Supondo que o menu está na cena de índice 0
    }
}
