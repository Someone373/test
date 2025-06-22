using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnPoint"))
        {
            respawnPoint = transform.position;
            Debug.Log("紀錄重生點: " + respawnPoint);
        }
    }

    void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = respawnPoint;
        Debug.Log("已重生至: " + respawnPoint);
    }
}
