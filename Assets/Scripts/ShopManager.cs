using UnityEngine;

public class ShopManagerTeste : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public void BuyHealthUpgrade()
    {
        playerHealth.IncreaseMaxHealth(20); // Aumenta 20 de vida máxima (e atual)
        // Aqui você pode adicionar lógica de custo futuramente, se quiser
    }
}
