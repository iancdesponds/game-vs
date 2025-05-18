using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public AudioClip footstepClip;
    public AudioClip coinClip;
    private AudioSource audioSource;

    public CoinManager coinManager;

    public float speed;

    private float footstepTimer = 0f;
    public float footstepInterval = 0.4f;

    // Novo campo público para o MapController ler
    [HideInInspector]
    public Vector2 moveDir;

    public PlayerXP playerXP; // <- Adicionado

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

        Vector2 rawMovement = new Vector2(moveHorizontal, moveVertical);
        bool isMoving = rawMovement != Vector2.zero;

        if (!isMoving)
        {
            // quando parar, zera moveDir também
            moveDir = Vector2.zero;

            animator.SetFloat("Speed", 0f);
            rb.MovePosition(rb.position);
            footstepTimer = 0f;
            return;
        }

        // normaliza para direção e atribui a moveDir
        moveDir = rawMovement.normalized;

        // move o corpo
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);

        // animação
        animator.SetFloat("Speed", rawMovement.magnitude);

        // flip de sprite
        if (moveHorizontal > 0) spriteRenderer.flipX = false;
        else if (moveHorizontal < 0) spriteRenderer.flipX = true;

        // áudio de passo
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
            coinManager.AddCoin(5);
            audioSource.PlayOneShot(coinClip);
        }
        else if (other.CompareTag("Xp"))
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinClip);

            if (playerXP != null)
            {
                playerXP.GainXP(10); // <- valor que aumenta a stamina
            }
        }
    }
}
