using UnityEngine;
using UnityEngine.UI;

public class PlayerXP : MonoBehaviour
{
    public int maxXP = 100;
    private int currentXP = 0;

    public XpBarUI xpBarUI;

    void Start()
    {
        currentXP = 0;
        xpBarUI.SetMaxXP(maxXP);
        xpBarUI.SetXP(currentXP);
    }

    public void GainXP(int amount)
    {
        currentXP = Mathf.Min(currentXP + amount, maxXP);
        xpBarUI.SetXP(currentXP);
    }
}
