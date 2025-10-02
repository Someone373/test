using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Renderer rend;

    public Color activeColor = Color.green;
    public Color inactiveColor = Color.red;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = inactiveColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnManager rm = FindObjectOfType<SpawnManager>();
            rm.SetCheckpoint(this);
        }
    }

    // ✅ 由 RespawnManager 呼叫
    public void Activate()
    {
        if (rend != null)
            rend.material.color = activeColor;
    }

    public void Deactivate()
    {
        if (rend != null)
            rend.material.color = inactiveColor;
    }
}
