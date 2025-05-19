using UnityEngine;

public class OrbitingOrbe : MonoBehaviour
{
    public float orbitRadius = 1.5f;
    public float orbitSpeed = 180f;
    public int damage = 1;
    public LayerMask enemyLayers;

    private Transform center;
    private float angle;

    public void Initialize(Transform player)
    {
        center = player;
    }

    void Update()
    {
        if (center == null) return;

        angle += orbitSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)) * orbitRadius;
        transform.position = center.position + offset;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyLayers) != 0)
        {
            other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }
    }
}
