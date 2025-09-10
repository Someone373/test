using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody rb;
    void Start()
    {
        Physics.gravity = new Vector3(0, -20f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce (0,-5f,0);
    }
}
