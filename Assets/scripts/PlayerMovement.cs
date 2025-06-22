using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    public Transform cameraPivot;

    // 新增：基礎重力加速度，這是你希望角色在空中初始感受到的重力
    public float baseGravity = -900f;
    // 新增：重力遞增速度，每秒重力增加多少
    public float gravityIncreaseRate = -450f;
    // 新增：最大重力，避免下落速度無限增加
    public float maxGravity = -90000f;

    private Rigidbody rb;
    private bool isGrounded;
    private float currentAdditionalGravity = 0f; // 追蹤額外增加的重力

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // 設定全域重力為一個較小的值，或直接使用你原來的設定
        // 因為我們主要會通過 AddForce 來動態控制重力
        Physics.gravity = new Vector3(0, baseGravity, 0); // 初始重力設定為你的基礎值

        rb.linearDamping = 5f;
        rb.angularDamping = 0.1f;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 camForward = cameraPivot.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraPivot.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = (camForward * v + camRight * h).normalized;
        Vector3 move = moveDir * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        // 處理跳躍
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            currentAdditionalGravity = 0f; // 跳起時重置額外重力
        }

        // 動態增加重力
        if (!isGrounded)
        {
            // 如果不在地面上，逐步增加額外重力
            currentAdditionalGravity -= gravityIncreaseRate * Time.fixedDeltaTime;
            // 限制額外重力不超過最大重力
            currentAdditionalGravity = Mathf.Max(currentAdditionalGravity, maxGravity - baseGravity);

            // 將這個額外重力以 ForceMode.Acceleration 的方式施加，因為它代表加速度
            rb.AddForce(Vector3.up * currentAdditionalGravity, ForceMode.Acceleration);
        }
        else
        {
            // 如果在地面上，重置額外重力
            currentAdditionalGravity = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 判斷是否落地：檢查碰撞點的法線方向
        // 這比單純檢查tag更魯棒，確保是真的"踩"在地面上
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.7f) // 如果碰撞點的法線朝上足夠多
            {
                isGrounded = true;
                currentAdditionalGravity = 0f; // 落地時重置額外重力
                break;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // 判斷是否離開地面（例如從邊緣掉落）
        // 這個邏輯需要更精確的檢查，避免快速跳躍後立即設置isGrounded=false
        // 最簡單的方法是在 FixedUpdate 檢查下方是否有地面
        // 但為了簡化，目前只依賴 OnCollisionEnter
    }

    // 建議：使用 Physics.CheckSphere 或 Raycast 來更精確判斷是否著地
    // void CheckGroundStatus()
    // {
    //     // 假設你的角色腳下有一個點，或者直接從角色中心向下檢測
    //     Vector3 sphereOrigin = transform.position + Vector3.down * (GetComponent<Collider>().bounds.extents.y - 0.1f);
    //     isGrounded = Physics.CheckSphere(sphereOrigin, 0.2f, LayerMask.GetMask("Ground")); 
    //     // 需要在 Unity 中設置你的地面物件的 Layer 為 "Ground"
    // }
}