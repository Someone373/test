using UnityEngine;

public class CheckpointLv2 : MonoBehaviour
{
    private Renderer rend;
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.red;
    [HideInInspector] public Quaternion savedWorldRotation;

    private bool isActive = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null) rend.material.color = inactiveColor;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            if (SpawnManagerLv2.Instance != null)
            {
                // 儲存當前世界旋轉
                savedWorldRotation = SpawnManagerLv2.Instance.worldRoot.rotation;
                SpawnManagerLv2.Instance.SetCheckpoint(this);
            }
        }
    }

    public void Activate()
    {
        isActive = true;
        if (rend != null) rend.material.color = activeColor;
    }

    public void Deactivate()
    {
        isActive = false;
        if (rend != null) rend.material.color = inactiveColor;
    }
}