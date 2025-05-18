using UnityEngine;

public abstract class AbilityBase : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public int level = 1;

    public abstract void Apply(GameObject player);

}
