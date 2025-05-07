using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public AudioClip footstepClip;
    public AudioClip coinClip;
    private AudioSource audioSource;

    public float speed;

    private float footstepTimer = 0f;
    public float footstepInterval = 0.4f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void PlayFootstep()
    {
        if (footstepClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(footstepClip);
        }
    }

    

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        bool isMoving = movement != Vector2.zero;

        if (!isMoving)
        {
            animator.SetFloat("Speed", 0f);
            rb.MovePosition(rb.position);
            footstepTimer = 0f; // reseta timer se parou
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

        footstepTimer += Time.fixedDeltaTime;
        if (footstepTimer >= footstepInterval)
        {
            PlayFootstep();
            footstepTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coletavel"))
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinClip);
        }
    }
}
