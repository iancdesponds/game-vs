using UnityEngine;
using System.Collections.Generic;

public class OrbitManager : MonoBehaviour
{
    public GameObject orbPrefab;
    public int maxOrbs = 5;
    public List<GameObject> orbs = new List<GameObject>();

    void Update()
    {
        for (int i = 0; i < orbs.Count; i++)
        {
            float angle = Time.time * 100 + (360f / orbs.Count) * i;
            float radius = 1.5f;
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
            orbs[i].transform.position = transform.position + offset;
        }
    }

    public void SpawnOrb()
    {
        if (orbs.Count >= maxOrbs) return;

        GameObject orb = Instantiate(orbPrefab);
        orb.transform.position = transform.position;
        orb.transform.parent = null; // ou this.transform se quiser que siga mesmo sem script

        orbs.Add(orb);
    }
}
