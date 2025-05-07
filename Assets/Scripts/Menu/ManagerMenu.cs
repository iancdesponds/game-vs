using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManagerMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image backgroundImage;
    public Sprite defaultBackground;      // "main_menu.png"
    public Sprite playHoverBackground;    // "main_menu_play.png"

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has been quit."); // Only visible in editor
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backgroundImage.sprite = playHoverBackground;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backgroundImage.sprite = defaultBackground;
    }
}
