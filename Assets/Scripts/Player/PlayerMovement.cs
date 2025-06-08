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

    [HideInInspector]
    public Vector2 moveDir;

    public PlayerXP playerXP;
    public Vector2 LastMoveDir { get; private set; }

    public Joystick joystick; // <- joystick mobile

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
        float moveX;
        float moveY;

#if UNITY_ANDROID || UNITY_IOS
        // Mobile: usar joystick
        moveX = joystick.Horizontal;
        moveY = joystick.Vertical;
#else
        // PC: usar teclado
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
#endif

        Vector2 input = new Vector2(moveX, moveY);

        if (input != Vector2.zero)
        {
            LastMoveDir = input.normalized;
        }

        moveDir = input.normalized;
    }

    void FixedUpdate()
    {
        bool isMoving = moveDir != Vector2.zero;

        if (!isMoving)
        {
            moveDir = Vector2.zero;
            animator.SetFloat("Speed", 0f);
            rb.MovePosition(rb.position);
            footstepTimer = 0f;
            return;
        }

        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
        animator.SetFloat("Speed", moveDir.magnitude);

        if (moveDir.x > 0) spriteRenderer.flipX = false;
        else if (moveDir.x < 0) spriteRenderer.flipX = true;

        footstepTimer += Time.fixedDeltaTime;
        if (footstepTimer >= footstepInterval)
        {
            PlayFootstep();
            footstepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(footstepClip);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coletavel"))
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinClip);
        }
        else if (other.CompareTag("Xp"))
        {
            Destroy(other.gameObject);

            if (playerXP != null)
            {
                playerXP.GainXP(10);
            }

            audioSource.PlayOneShot(xpClip);
            playerStats.GainXP(xpAmount);
        }
    }
}
