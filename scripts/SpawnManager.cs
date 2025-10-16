using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform startPoint;      
    private Transform currentRespawn;
    private GameObject player;

    private Checkpoint activeCheckpoint; // 記錄當前啟動的重生點

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null && startPoint != null)
        {
            currentRespawn = startPoint;
            MovePlayerTo(currentRespawn);
        }
    }

    public void SetCheckpoint(Checkpoint newCheckpoint)
    {
        // 關掉舊的
        if (activeCheckpoint != null)
        {
            activeCheckpoint.Deactivate();
        }

        // 更新新的
        activeCheckpoint = newCheckpoint;
        currentRespawn = newCheckpoint.transform;
        activeCheckpoint.Activate();
    }

    void Update()
    {
        if (player != null && currentRespawn != null)
        {
            if (player.transform.position.y<=-30 || Input.GetKeyDown(KeyCode.R))
            {
                MovePlayerTo(currentRespawn);
                Debug.Log(currentRespawn);
            }
            
        }
    }

    private void MovePlayerTo(Transform point)
    {
        player.transform.position = point.position;
        player.transform.rotation = point.rotation;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}


