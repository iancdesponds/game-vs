using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private MapController mc;
    public GameObject targetMap;

    void Start()
    {
        // Substitui FindObjectOfType obsoleto
        mc = FindFirstObjectByType<MapController>();
        if (mc == null)
        {
            // Força o Debug da Unity, evitando ambiguidade
            UnityEngine.Debug.LogError("MapController não encontrado na cena!");
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            mc.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && mc.currentChunk == targetMap)
        {
            mc.currentChunk = null;
        }
    }
}
