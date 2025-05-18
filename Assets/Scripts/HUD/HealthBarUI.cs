using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;

    // Largura inicial da barra de vida
    private float baseWidth = 160f;
    // Quanto a barra cresce para cada ponto de vida extra
    private float widthPerHealth = 0.5f;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        AdjustBarWidth(maxHealth);
    }

    public void SetHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }

    private void AdjustBarWidth(int maxHealth)
    {
        RectTransform rt = slider.GetComponent<RectTransform>();
        if (rt != null)
        {
            // Crescimento baseado na vida acima de 100
            float extraHealth = maxHealth - 100;
            float newWidth = baseWidth + extraHealth * widthPerHealth;

            // Ajusta somente a largura, mantendo o lado esquerdo fixo
            rt.pivot = new Vector2(0f, 0.5f); // esquerda-central
            rt.sizeDelta = new Vector2(newWidth, rt.sizeDelta.y);
        }
    }
}
