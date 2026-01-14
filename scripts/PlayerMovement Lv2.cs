/*using UnityEngine;

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
}*/

/*using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;          // 移動速度
    public float jumpHeight = 2f;     // 跳躍高度
    public float gravity = -9.81f;    // 重力值

    public Transform cam;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // 防止角色翻倒
    }

    void Update()
    {

        // 取得輸入
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 取相機方向
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        // 計算移動方向
        Vector3 moveDir = camForward * moveZ + camRight * moveX;
        moveDir.Normalize();

        // 計算移動位置（使用 MovePosition 比較平滑）
        Vector3 newPos = rb.position + moveDir * speed * Time.deltaTime;
        rb.MovePosition(newPos);

        // 跳躍
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 重力（手動加，避免物理重力影響不均）
        velocity.y += gravity * Time.deltaTime;

        // 套用垂直速度
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, velocity.y, rb.linearVelocity.z);
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
}*/

/*using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 5f;
    public float gravity = -9.81f;

    public Transform cam;

    [HideInInspector] public bool canMove = true; // ★ 世界旋轉時會關掉

    private Rigidbody rb;
    private bool isGrounded;
    private float verticalVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (!canMove) return;

        // ===== 移動輸入 =====
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = (camForward * moveZ + camRight * moveX).normalized;

        Vector3 newPos = rb.position + moveDir * speed * Time.deltaTime;
        rb.MovePosition(newPos);

        // ===== 跳躍 =====
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = false;
        }

        // ===== 重力 =====
        verticalVelocity += gravity * Time.deltaTime;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, verticalVelocity, rb.linearVelocity.z);
    }

    void FixedUpdate()
    {
        // ★ 保險：每幀鎖死 XZ 旋轉
        Quaternion rot = rb.rotation;
        rb.MoveRotation(Quaternion.Euler(0f, rot.eulerAngles.y, 0f));
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            verticalVelocity = Mathf.Min(verticalVelocity, 0f);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}*/

/*using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 5f;
    public float gravity = -9.81f;
    public Transform cam;

    [HideInInspector] public bool canMove = true;

    private Rigidbody rb;
    private bool isGrounded;
    private float verticalVelocity;

    private float moveX, moveZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (!canMove) return;

        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        // 跳躍
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        // 水平移動
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = (camForward * moveZ + camRight * moveX).normalized;

        //改velocity水平分量
        Vector3 horizontalVelocity = moveDir * speed;
        rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);

        // 重力
        verticalVelocity += gravity * Time.fixedDeltaTime;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, verticalVelocity, rb.linearVelocity.z);

        // 保險鎖旋轉
        Quaternion rot = rb.rotation;
        rb.MoveRotation(Quaternion.Euler(0f, rot.eulerAngles.y, 0f));
    }

    //跳躍
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            verticalVelocity = Mathf.Min(verticalVelocity, 0f);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}*/

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2f; // 物理公式下，2公尺已經很高了
    public float gravityScale = 2f; // 用來增強落下的重量感
    public Transform cam;

    [HideInInspector] public bool canMove = true;

    private Rigidbody rb;
    private bool isGrounded;
    private float moveX, moveZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 建議開啟內建重力，除非你有特殊需求要手動寫重力公式
        rb.useGravity = true; 
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (!canMove) return;

        moveX = Input.GetAxisRaw("Horizontal"); // GetAxisRaw 反應更即時
        moveZ = Input.GetAxisRaw("Vertical");

        // 跳躍邏輯：改用 AddForce 會讓物理表現更穩定
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // 物理公式：力 = sqrt(高度 * -2 * 重力加速度)
            float jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        // 計算方向
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = (camForward * moveZ + camRight * moveX).normalized;

        // 設定水平速度，保留垂直方向的物理運算結果
        Vector3 targetVelocity = moveDir * speed;
        Vector3 currentVelocity = rb.linearVelocity;
        
        rb.linearVelocity = new Vector3(targetVelocity.x, currentVelocity.y, targetVelocity.z);

        // 可選：自訂重力倍率（讓跳躍感覺不那麼飄）
        if (rb.linearVelocity.y < 0) {
            rb.AddForce(Vector3.up * Physics.gravity.y * (gravityScale - 1) * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // 檢查碰撞點的法線，確保是「腳下」的地面而非牆壁
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.9f) // 向上法線代表是地面
            {
                isGrounded = true;
                break;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}

