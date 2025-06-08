using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 20f;
    public Rigidbody rb;
    public Transform cam;  

    private bool isGrounded;
    void Start()
    {
        Physics.gravity = new Vector3(0, -20f, 0);
    }
    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal"); 
        float vertical = Input.GetAxisRaw("Vertical");    

        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = camForward * vertical + camRight * horizontal;
        moveDir.Normalize();

        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

