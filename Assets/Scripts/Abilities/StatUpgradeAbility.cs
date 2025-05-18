using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Stat Upgrade")]
public class StatUpgradeAbility : AbilityBase
{
    public enum StatType { MaxHealth, MoveSpeed, AttackDamage }

    public StatType statType;
    public int value;

    public override void Apply(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        PlayerAttack attack = player.GetComponent<PlayerAttack>();
        PlayerHealth health = player.GetComponent<PlayerHealth>();

        switch (statType)
        {
            case StatType.MaxHealth:
                health.maxHealth += value;
                break;
            case StatType.MoveSpeed:
                movement.speed += value;
                break;
            case StatType.AttackDamage:
                attack.attackDamage += value;
                break;
        }
    }
}
