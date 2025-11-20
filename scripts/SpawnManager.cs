using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform startPoint;
    private Transform currentRespawn;
    private GameObject player;

    private Checkpoint activeCheckpoint;

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
        if (activeCheckpoint != null)
        {
            activeCheckpoint.Deactivate();
        }

        activeCheckpoint = newCheckpoint;
        currentRespawn = newCheckpoint.transform;
        activeCheckpoint.Activate();
    }

    void Update()
    {
        if (player != null && currentRespawn != null)
        {
            if (player.transform.position.y <= -30 || Input.GetKeyDown(KeyCode.R))
            {
                MovePlayerTo(currentRespawn);
            }
        }
    }

    private void MovePlayerTo(Transform point)
    {
        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false; // 關閉碰撞，允許瞬移
            player.transform.position = point.position;
            player.transform.rotation = point.rotation;
            controller.enabled = true; // 開回來
        }
    }
}
