using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class DummyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 40f;
    

    private Rigidbody rb;
    private Animator animator;
    private FollowPlayer camScript;
    private bool isGrounded = true; // 是否在地上
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        camScript = FindObjectOfType<FollowPlayer>();

        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 取得相機前方與右方 (忽略Y軸)
        Vector3 camForward = camScript.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = camScript.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        // 依照玩家輸入 + 相機方向，計算移動方向
        Vector3 move = (camForward * v + camRight * h).normalized;

        // 移動
        if (move.magnitude > 0.1f)
        {
            rb.MovePosition(transform.position + move * moveSpeed * Time.fixedDeltaTime);

            // 面向移動方向
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // 沒有輸入 → 凍結旋轉
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        animator.SetFloat("Speed", move.magnitude * moveSpeed);


        if(transform.position.y < -5)
        {
            animator.SetBool("PosY-", true);
        }
        else
        {
            animator.SetBool("PosY-",false);
        }
    }

    void Update()
    {
        // 跳躍判斷 (空白鍵)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {    
            animator.SetTrigger("jump");
        }
    }

    // 偵測是否落地
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("IsGrounded", false);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsGrounded", true);
        }
    }
}