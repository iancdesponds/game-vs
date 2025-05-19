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

    private PlayerStats playerStats;

    public AudioClip xpClip;

    public int xpAmount = 10;

    public float speed;

    private float footstepTimer = 0f;
    public float footstepInterval = 0.4f;

    // Novo campo p�blico para o MapController ler
    [HideInInspector]
    public Vector2 moveDir;

    public PlayerXP playerXP; // <- Adicionado
    public Vector2 LastMoveDir { get; private set; }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        playerStats = GetComponent<PlayerStats>();
    }
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(moveX, moveY);

        if (input != Vector2.zero)
        {
            LastMoveDir = input.normalized;
        }

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
            // quando parar, zera moveDir tamb�m
            moveDir = Vector2.zero;

            animator.SetFloat("Speed", 0f);
            rb.MovePosition(rb.position);
            footstepTimer = 0f;
            return;
        }

        // normaliza para dire��o e atribui a moveDir
        moveDir = rawMovement.normalized;

        // move o corpo
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);

        // anima��o
        animator.SetFloat("Speed", rawMovement.magnitude);

        // flip de sprite
        if (moveHorizontal > 0) spriteRenderer.flipX = false;
        else if (moveHorizontal < 0) spriteRenderer.flipX = true;

        // �udio de passo
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
        else if (other.CompareTag("Xp") || other.CompareTag("XP"))
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinClip);

            if (playerXP != null)
            {
                playerXP.GainXP(10); // <- valor que aumenta a stamina
            }

            Destroy(other.gameObject);
            audioSource.PlayOneShot(xpClip);
            playerStats.GainXP(xpAmount);

        }
    }
}
