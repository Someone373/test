using UnityEngine;

public class SpawnManagerLv1 : MonoBehaviour
{
    public Transform startPoint;
    private Transform currentRespawn;
    private GameObject player;

    private CheckpointLv1 activeCheckpoint;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null && startPoint != null)
        {
            currentRespawn = startPoint;
            MovePlayerTo(currentRespawn);
        }
    }

    public void SetCheckpoint(CheckpointLv1 newCheckpoint)
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
        else
        {
            // 備用：萬一還是用 Rigidbody
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
}
