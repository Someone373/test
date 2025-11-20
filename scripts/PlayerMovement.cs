using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;      // 移動速度
    public float gravity = -9.81f;  // 重力值
    public float jumpHeight = 2.0f; // 跳躍高度

    public float slopeLimit = 40f; // 最大可站立角度
    public float slideSpeed = 5f;  // 滑動速度
    
    public Transform cam;
    public Rigidbody rb;

    private CharacterController controller;
    private Vector3 velocity;       // 垂直速度（重力和跳躍）
    private bool isGrounded;        // 檢測角色是否在地面上
    private bool onSlope;

    void Start()
    {
        // 獲取 Character Controller 組件
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 檢測是否在地面上
        CheckSlope();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // 重置垂直速度，稍微向下推以保持接地狀態
        }

        // 獲取水平移動的輸入
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 相機方向
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = camForward * moveZ + camRight * moveX;
        moveDir.Normalize();


        // 移動角色
        controller.Move(moveDir * speed * Time.deltaTime);

        // 檢測跳躍輸入
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // 計算跳躍速度
        }

        // 應用重力
        velocity.y += gravity * Time.deltaTime;

        // 移動角色垂直方向（重力影響）
        controller.Move(velocity * Time.deltaTime);
    }

    void CheckSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if (angle > slopeLimit && onSlope)
            {
                SlideDown(hit.normal);
            }
        }
    }

    void SlideDown(Vector3 slopeNormal)
    {
        Vector3 slideDirection = new Vector3(slopeNormal.x, -slopeNormal.y, slopeNormal.z);
        controller.Move(slideDirection * slideSpeed * Time.deltaTime);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Slope"))
        {
            onSlope = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Slope"))
        {
            onSlope = false;
        }
    }    
}