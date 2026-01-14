using UnityEngine;

public class CheckpointLv1 : MonoBehaviour
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
            SpawnManagerLv1 rm = FindObjectOfType<SpawnManagerLv1>();
            rm.SetCheckpoint(this);
        }
    }
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
