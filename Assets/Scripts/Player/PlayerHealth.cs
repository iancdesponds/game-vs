using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public int maxHealth = 100;
    private int currentHealth;
    public AudioClip damageClip;
    private AudioSource audioSource;

    public UIManager uiManager;

    public HealthBarUI healthBarUI;

    void Start()
    {
        currentHealth = maxHealth;
        healthBarUI.SetMaxHealth(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayDamage()
    {
        audioSource.PlayOneShot(damageClip);

        LayoutRebuilder.ForceRebuildLayoutImmediate(
        healthBarUI.slider.GetComponent<RectTransform>()
    );
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(FlashRed());
        PlayDamage();


        if (currentHealth <= 0)
        {
            Die();
        }

        healthBarUI.SetHealth(currentHealth);
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red; // pinta de vermelho
        yield return new WaitForSeconds(0.1f); // espera 0.1 segundos
        spriteRenderer.color = Color.white; // volta para cor normal
    }

    void Die()
    {
        Debug.Log("Player morreu!");
        uiManager.ShowEndGame();

        Time.timeScale = 0f;
    }
}
