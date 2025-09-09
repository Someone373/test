/*using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DummyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Animator animator;
    private Transform cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cam = Camera.main.transform;

        rb.freezeRotation = true; // 防止角色因碰撞翻倒
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 依照鏡頭方向移動
        Vector3 move = cam.right * h + cam.forward * v;
        move.y = 0f; // 保持水平移動
        move.Normalize();

        if (move.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
            transform.forward = move; // 角色面向移動方向
        }

        // 動畫參數：speed
        if (animator != null)
            animator.SetFloat("speed", move.magnitude * moveSpeed);
    }
}*/
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DummyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Transform cam;

    // 提供給動畫控制器讀取
    [HideInInspector] public float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;

        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 依照鏡頭方向移動
        Vector3 move = cam.right * h + cam.forward * v;
        move.y = 0f;
        move.Normalize();

        if (move.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
            transform.forward = move;
        }

        // 記錄移動速度 (提供給動畫)
        currentSpeed = move.magnitude * moveSpeed;
    }
}

