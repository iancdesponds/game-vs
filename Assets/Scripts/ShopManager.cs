using UnityEngine;

public class ShopManagerTeste : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public void BuyHealthUpgrade()
    {
        playerHealth.IncreaseMaxHealth(20); // Aumenta 20 de vida m�xima (e atual)
        // Aqui voc� pode adicionar l�gica de custo futuramente, se quiser
    }
}
