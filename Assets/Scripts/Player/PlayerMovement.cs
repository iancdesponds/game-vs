using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer; // Novo!
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Pega o SpriteRenderer
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // Atualiza a velocidade no Animator
        animator.SetFloat("Speed", movement.magnitude);

        // Inverte o sprite se mover para a esquerda
        if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false; // olhando para a direita
        }
        else if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true; // olhando para a esquerda
        }
    }
}
