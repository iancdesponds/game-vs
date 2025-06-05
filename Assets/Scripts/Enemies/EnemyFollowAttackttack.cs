using UnityEngine;

public class EnemyFollowAttack : MonoBehaviour
{
    public Transform player;
    public float baseSpeed = 2f;
    public float stopDistance = 0.5f;
    public float attackRate = 1f;

    private float currentSpeedMultiplier = 1f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private float nextAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;

            if (movement.x > 0)
                spriteRenderer.flipX = false;
            else if (movement.x < 0)
                spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackRate;
            }
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * baseSpeed * currentSpeedMultiplier * Time.fixedDeltaTime);
    }

    void Attack()
    {
        Debug.Log("Enemy attacked the player!");
        player.GetComponent<PlayerHealth>().TakeDamage(10);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        currentSpeedMultiplier = multiplier;
    }
}
