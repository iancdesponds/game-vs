using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int maxXP = 30;
    private int currentXP = 0;

    public XpBarUI xpBarUI;
    public GameObject levelUpMenu; // atribua no Inspector

    void Start()
    {
        currentXP = 0;
        xpBarUI.SetMaxXP(maxXP);
        xpBarUI.SetXP(currentXP);
        levelUpMenu.SetActive(false); // começa invisível
    }

    public void GainXP(int amount)
    {
        currentXP = Mathf.Min(currentXP + amount, maxXP);
        xpBarUI.SetXP(currentXP);

        if (currentXP >= maxXP)
        {
            TriggerLevelUp();
        }
    }

    void TriggerLevelUp()
    {
        Time.timeScale = 0f; // pausa o jogo
        levelUpMenu.SetActive(true); // mostra menu
    }

    public void ConfirmUpgrade()
    {
        levelUpMenu.SetActive(false);
        Time.timeScale = 1f;
        currentXP = 0;
        xpBarUI.SetXP(currentXP);
    }
}
