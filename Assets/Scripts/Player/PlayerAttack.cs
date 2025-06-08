using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown = 3f;
    private float attackTimer = 0f;

    public Animator animator;
    public Transform attackPoint;
    public float attackRadius = 2f; // Novo: raio da área de ataque
    public LayerMask enemyLayers;
    public int attackDamage = 1;

    private AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip fireballClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(attackClip);
        StartCoroutine(DelayedAttack());
    }

    IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(0.2f); // Ajuste conforme animação

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

        if (enemiesInRange.Length == 0)
            yield break;

        // Acha o inimigo mais próximo
        Collider2D closestEnemy = enemiesInRange[0];
        float minDist = Vector2.Distance(transform.position, closestEnemy.transform.position);

        foreach (Collider2D enemy in enemiesInRange)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestEnemy = enemy;
            }
        }

        // Aplica dano ao inimigo mais próximo
        EnemyHealth enemyHealth = closestEnemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
