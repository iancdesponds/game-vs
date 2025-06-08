using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float fireballSpeed = 7f;
    public float cooldown = 2f;
    public int damage = 1;

    public float fireRate = 3f;

    private float timer;
    private PlayerMovement playerMovement;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
            Debug.LogWarning("FireballShooter: PlayerMovement não encontrado no GameObject.");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            ShootFireball();
            timer = 0f;
        }
    }

    void ShootFireball()
    {
        Vector3 dir = GetLastMoveDirection(); // ou Vector3.right se for fixo
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        if (dir != Vector3.zero)
{
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            fireball.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        Debug.Log(dir);

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = dir.normalized * fireballSpeed;
        }

        Fireball fb = fireball.GetComponent<Fireball>();
        if (fb != null)
        {
            fb.damage = damage;
        }
    }

    Vector3 GetLastMoveDirection()
    {
        // Direção oposta (negativa) ao último deslocamento do player
        return playerMovement != null
            ? (Vector3)(-playerMovement.LastMoveDir)   // ← aqui está a inversão
            : Vector3.left;                            // padrão caso o componente não exista
    }
}
