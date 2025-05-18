using UnityEngine;
using UnityEngine.UI;

public class XpBarUI : MonoBehaviour
{
    public Slider slider;

    public void SetMaxXP(int maxXP)
    {
        slider.maxValue = maxXP;
    }

    public void SetXP(int currentXP)
    {
        Debug.Log("XP atual no slider: " + currentXP);
        slider.value = currentXP;
    }
}
