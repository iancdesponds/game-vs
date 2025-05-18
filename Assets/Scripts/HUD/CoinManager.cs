using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int totalCoins = 0;
    public TMP_Text coinText; // se estiver usando TextMeshPro, troque para TMP_Text

    void Start()
    {
        UpdateCoinText();
    }

    public void AddCoin(int amount)
    {
        totalCoins += amount;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text = "x" + totalCoins;
    }
}
