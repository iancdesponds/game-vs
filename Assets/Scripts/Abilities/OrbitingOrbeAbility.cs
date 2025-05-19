using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Orbiting Orbs")]
public class OrbitingOrbAbility : AbilityBase
{
    public GameObject orbPrefab;
    public int maxOrbs = 5;

    public override void Apply(GameObject player)
    {
        OrbitManager manager = player.GetComponent<OrbitManager>();
        if (manager == null)
        {
            manager = player.gameObject.AddComponent<OrbitManager>();
            manager.orbPrefab = orbPrefab;
            manager.maxOrbs = maxOrbs;
        }

        if (manager.orbs.Count < maxOrbs)
        {
            manager.SpawnOrb();
        }
    }
}
