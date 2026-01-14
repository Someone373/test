using UnityEngine;
using System.Collections;

public class WallRotateTrigger : MonoBehaviour
{
    public WorldRotator world;
    public Vector3 rotateAxis = Vector3.forward;

    void OnTriggerEnter(Collider other)
    { 
        if (!other.CompareTag("Player")) return;

        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        pm.canMove = false;

        Rigidbody rb = other.GetComponent<Rigidbody>();

        StartCoroutine(Rotate(pm));
    }

    IEnumerator Rotate(PlayerMovement pm)
    {
        Rigidbody rb = pm.GetComponent<Rigidbody>();

        pm.canMove = false;
        rb.linearVelocity = Vector3.zero;

        yield return StartCoroutine(world.Rotate90(rotateAxis));

        pm.canMove = true;
    }
}
