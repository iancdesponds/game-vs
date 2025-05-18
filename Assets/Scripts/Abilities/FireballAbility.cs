using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireball")]
public class FireballAbility : AbilityBase
{
    public GameObject fireballPrefab;
    public float fireRate = 2f;
    public int baseDamage = 1;

    public override void Apply(GameObject player)
    {
        FireballShooter existing = player.GetComponent<FireballShooter>();
        if (existing != null)
        {
            existing.damage += baseDamage; // aumenta dano
        }
        else
        {
            FireballShooter shooter = player.AddComponent<FireballShooter>();
            shooter.fireballPrefab = fireballPrefab;
            shooter.fireRate = fireRate;
            shooter.damage = baseDamage;
        }
    }
}
