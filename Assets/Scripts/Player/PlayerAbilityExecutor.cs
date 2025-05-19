using UnityEngine;
using System.Collections.Generic;

public class PlayerAbilityExecutor : MonoBehaviour
{
    public List<AbilityBase> activeAbilities = new List<AbilityBase>();

    public void AddAbility(AbilityBase ability)
    {
        activeAbilities.Add(ability);
        ability.Apply(gameObject);
    }
}
