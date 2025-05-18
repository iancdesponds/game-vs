using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;



public class PlayerStats : MonoBehaviour
{
    [Header("Level & XP")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    [Header("Stats")]
    public int maxHealth = 10;
    public int currentHealth = 10;
    public float moveSpeed = 5f;
    public int attackDamage = 1;

    [Header("UI")]
    public GameObject levelUpMenu;
    public Button[] abilityButtons; // 3 botões do menu (ligados no Inspector)
    public TMP_Text[] abilityTexts; // Textos com nomes das habilidades (um para cada botão)

    [Header("Abilities")]
    public List<AbilityBase> allAbilities; // Lista de TODAS as habilidades possíveis
    private List<AbilityBase> selectedAbilities = new List<AbilityBase>(); // 3 sorteadas

    private PlayerAbilityExecutor abilityExecutor;

    void Start()
    {
        abilityExecutor = GetComponent<PlayerAbilityExecutor>();
        currentHealth = maxHealth; // Garante que comece com vida cheia
    }

    public void GainXP(int amount)
    {
        currentXP += amount;

        

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        currentXP -= xpToNextLevel;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);

        Time.timeScale = 0f;

        levelUpMenu.SetActive(true);
        SelectRandomAbilities();
    }

    void SelectRandomAbilities()
    {
        selectedAbilities.Clear();

        List<AbilityBase> pool = new List<AbilityBase>(allAbilities);

        for (int i = 0; i < 3 && pool.Count > 0; i++)
        {
            int randIndex = Random.Range(0, pool.Count);
            selectedAbilities.Add(pool[randIndex]);
            abilityTexts[i].text = pool[randIndex].abilityName;

            int capturedIndex = i; // Captura o índice para o lambda
            abilityButtons[i].onClick.RemoveAllListeners();
            abilityButtons[i].onClick.AddListener(() => SelectAbility(capturedIndex));

            pool.RemoveAt(randIndex);
        }
    }

    public void SelectAbility(int index)
    {
        if (index < selectedAbilities.Count)
        {
            AbilityBase chosen = Instantiate(selectedAbilities[index]); // nova instância
            abilityExecutor.AddAbility(chosen);
            Debug.Log($"Habilidade escolhida: {chosen.abilityName}");
        }

        levelUpMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    // Método para curar o jogador
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
