using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("End Game UI")]
    public GameObject endGamePanel;

    void Awake()
    {
        Debug.Log("asd");
    }

    void Start()
    {
        if (endGamePanel != null)
            Time.timeScale = 1f;
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
        Debug.Log("aaa");
        SceneManager.LoadSceneAsync(1);
    }

    // Botão: Voltar ao menu
    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(0); // Supondo que o menu está na cena de índice 0
    }
}
