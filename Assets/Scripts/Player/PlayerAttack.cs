using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown = 3f;
    private float attackTimer = 0f;

    public Animator animator;
    public Transform attackPoint;
    public float attackWidth = 1.5f;  // Largura da área de ataque
    public float attackHeight = 0.8f; // Altura da área de ataque
    public float attackDistance = 0.5f; // Distância da área de ataque a partir do centro do jogador
    public LayerMask enemyLayers;
    public int attackDamage = 1;


    private AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip fireballClip;

    private Vector2 lastMoveDir = Vector2.right;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Atualiza direção
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 inputDir = new Vector2(moveX, moveY);
        if (inputDir != Vector2.zero)
            lastMoveDir = inputDir.normalized;

        // Alinha o ponto de ataque à frente do jogador
        attackPoint.position = transform.position + (Vector3)(lastMoveDir.normalized * attackDistance);

        // Temporizador de ataque
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
        // Espera um tempo antes de causar dano (ajuste conforme a animação)
        yield return new WaitForSeconds(0.2f);

        // Ângulo de rotação baseado na direção do jogador
        float angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg;

        // Detecta inimigos na área retangular do ataque (apenas na frente)
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(
            attackPoint.position,
            new Vector2(attackWidth, attackHeight),
            angle,
            enemyLayers
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            // Causa dano
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(attackDamage);
        }
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        
        // Desenha um retângulo na direção do movimento
        float angle = 0;
        if (Application.isPlaying)
        {
            angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg;
        }
        else
        {
            // No editor, quando não estiver rodando, assume direção para direita
            attackPoint.position = transform.position + Vector3.right * attackDistance;
        }
            
        // Converter para Matrix4x4 para rotacionar o gizmo
        Matrix4x4 originalMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(
            attackPoint.position, 
            Quaternion.Euler(0, 0, angle), 
            Vector3.one
        );
        
        // Desenha o wireframe do retângulo
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(attackWidth, attackHeight, 0));
        
        // Restaura a matriz original
        Gizmos.matrix = originalMatrix;
    }
}