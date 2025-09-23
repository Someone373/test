using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    public Rigidbody rb;
    public Transform cam;  

    private bool isGrounded;
    private FollowPlayer camScript;   // 存相機腳本

    void Start()
    {
        // 一開始就找到 ThirdPersonCamera，避免每幀搜尋
        camScript = FindObjectOfType<FollowPlayer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); 
        float vertical = Input.GetAxisRaw("Vertical");    

        // 相機方向
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = camForward * vertical + camRight * horizontal;
        moveDir.Normalize();

        // 移動
        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = rb.linearVelocity.y;   
        rb.linearVelocity = velocity;       

        // 面向移動方向（只有真的有速度才會轉向）
        if (rb.linearVelocity.magnitude > 0.1f && camScript != null)   
        {
            transform.rotation = Quaternion.Euler(0, camScript.ReturnRotation(), 0);
        }
        else
        {
             rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
