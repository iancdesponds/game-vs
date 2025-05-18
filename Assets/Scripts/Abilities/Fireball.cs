using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 7f;
    public int damage = 1;
    public float lifetime = 3f;
    public LayerMask enemyLayers;

    private Vector2 direction;

    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyLayers) != 0)
        {
            other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
