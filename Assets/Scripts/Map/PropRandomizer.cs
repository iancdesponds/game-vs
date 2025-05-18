using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;

    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            // Desambiguamos expl�cito para UnityEngine.Random
            int rand = UnityEngine.Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(
                propPrefabs[rand],
                sp.transform.position,
                Quaternion.identity
            );
            prop.transform.SetParent(sp.transform);
        }
    }
}
