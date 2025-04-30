using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal"); // Mudei para GetAxisRaw
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if (movement == Vector2.zero)
        {
            animator.SetFloat("Speed", 0f);
            rb.MovePosition(rb.position); 
            return;
        }


        Vector2 normalizedMovement = movement.normalized;

        rb.MovePosition(rb.position + normalizedMovement * speed * Time.fixedDeltaTime);

        animator.SetFloat("Speed", movement.magnitude);

        if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coletavel"))
        {
            Destroy(other.gameObject);
        }
    }
}
