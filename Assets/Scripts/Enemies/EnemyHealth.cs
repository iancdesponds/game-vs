using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public GameObject dropPrefab;
    private ObjectPool pool;

    void OnEnable()
    {
        ResetHealth();
    }

    void Start()
    {
        pool = FindAnyObjectByType<ObjectPool>(); // Simple way to get pool reference
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }

        pool.ReturnObject(gameObject); // Instead of Destroy, return to pool
    }
}
