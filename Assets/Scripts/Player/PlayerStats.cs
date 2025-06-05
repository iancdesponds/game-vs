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
    public Button[] abilityButtons; // 3 buttons for selecting abilities
    public TMP_Text[] abilityTexts; // Text for each ability button

    [Header("Reroll UI")]
    public Button rerollButton;         // Reroll button
    public TMP_Text rerollCounterText;  // Text showing remaining rerolls
    public int rerollCount = 3;         // Starting rerolls

    [Header("Abilities")]
    public List<AbilityBase> allAbilities; // List of all possible abilities
    private List<AbilityBase> selectedAbilities = new List<AbilityBase>(); // 3 selected

    public XpBarUI xpBarUI;

    private PlayerAbilityExecutor abilityExecutor;

    void Awake()
    {
        abilityExecutor = GetComponent<PlayerAbilityExecutor>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        xpBarUI.SetMaxXP(xpToNextLevel);
        xpBarUI.SetXP(currentXP);

        rerollButton.onClick.AddListener(RerollAbilities);
        UpdateRerollUI();
    }

    public void GainXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }

        xpBarUI.SetXP(currentXP);
    }

    void LevelUp()
    {
        level++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);
        xpBarUI.SetMaxXP(xpToNextLevel);

        // Remove the line below to make reroll count persistent
        // rerollCount = 3;

        UpdateRerollUI();

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

            int capturedIndex = i; // Capture index for lambda
            abilityButtons[i].onClick.RemoveAllListeners();
            abilityButtons[i].onClick.AddListener(() => SelectAbility(capturedIndex));

            pool.RemoveAt(randIndex);
        }
    }

    public void SelectAbility(int index)
    {
        if (index < selectedAbilities.Count)
        {
            if (selectedAbilities[index] == null)
            {
                Debug.LogError("selectedAbilities[index] is NULL!");
                return;
            }

            AbilityBase chosen = ScriptableObject.Instantiate(selectedAbilities[index]);
            abilityExecutor.AddAbility(chosen);
            Debug.Log($"Selected ability: {chosen.abilityName}");
        }

        levelUpMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RerollAbilities()
    {
        if (rerollCount <= 0)
        {
            Debug.Log("No rerolls left!");
            return;
        }

        rerollCount--;
        UpdateRerollUI();
        SelectRandomAbilities();

        Debug.Log("Abilities rerolled!");
    }

    void UpdateRerollUI()
    {
        rerollCounterText.text = rerollCount.ToString();
        rerollButton.interactable = rerollCount > 0;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
