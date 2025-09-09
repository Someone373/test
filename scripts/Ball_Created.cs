using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ballPrefab;     // 要丟的球
    public Transform throwPoint;      // 丟球的位置（例如角色前方）
    public float throwForce = 10f;    // 丟出的力道

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 滑鼠左鍵點擊
        {
            Throw();
        }
    }

    void Throw()
    {
        // 生成球
        GameObject ball = Instantiate(ballPrefab, throwPoint.position, throwPoint.rotation);

        // 加上物理推力
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        Destroy(ball, 2f);
        
    }
}
