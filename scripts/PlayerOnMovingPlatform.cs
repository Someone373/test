/*using UnityEngine;

public class PlayerOnMovingPlatform: MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(0, 0, 5); // 移動距離
    public float moveSpeed = 1f; // 移動速度
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * moveSpeed, 1f);
        transform.position = Vector3.Lerp(startPos, startPos + moveOffset, t);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                collision.transform.SetParent(transform);
                break;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (collision.transform.parent == transform)
        {
            collision.transform.SetParent(null);
        }
    }

}*/


/*using UnityEngine;

// 將此腳本附加到您的**移動平台**上
public class MovingPlatform : MonoBehaviour
{
    // 用來追蹤前一幀的位置和旋轉
    private Vector3 _previousPosition;
    private Quaternion _previousRotation;

    // 儲存目前站在平台上的玩家
    private Transform _playerTransform;

    void Start()
    {
        _previousPosition = transform.position;
        _previousRotation = transform.rotation;
    }

    // 平台的主要移動/旋轉邏輯通常會在 FixedUpdate 中
    // 假設您的平台移動是在 FixedUpdate 中進行的
    void FixedUpdate()
    {
        // *** 這裡應該是您的平台移動邏輯 ***
        // 例如： transform.position = Vector3.Lerp(a, b, t);
        // **********************************

        if (_playerTransform != null)
        {
            // 計算平台的位移和旋轉變化
            Vector3 positionDelta = transform.position - _previousPosition;
            Quaternion rotationDelta = transform.rotation * Quaternion.Inverse(_previousRotation);

            // --- 應用平台移動 ---
            // 將位移直接加到玩家位置
            _playerTransform.position += positionDelta;

            // --- 應用平台旋轉 (重要) ---
            // 計算玩家相對於平台中心的偏移
            Vector3 offset = _playerTransform.position - transform.position;

            // 將偏移量套用旋轉
            offset = rotationDelta * offset;

            // 將旋轉後的偏移量加回平台中心
            _playerTransform.position = transform.position + offset;

            // 如果平台會旋轉，玩家的旋轉也需要同步 (可選，取決於遊戲設計)
            // _playerTransform.rotation = rotationDelta * _playerTransform.rotation;
        }

        // 更新前一幀的位置和旋轉供下一幀使用
        _previousPosition = transform.position;
        _previousRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 確認玩家是"站在"平台上，而不是從側面撞到
            // 通常可以檢查碰撞點的法線方向，或只在下方碰撞時啟用
            // 這裡簡化為直接設定，您可能需要更嚴格的檢查
            _playerTransform = other.transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerTransform = null;
        }
    }
}*/

using UnityEngine;

public class PlayerOnMovingPlatform : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(0, 0, 5);
    public float moveSpeed = 1f;
    private Vector3 startPos;
    private Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        // 確保平台有 Rigidbody 且勾選 Is Kinematic
        if (rb != null) rb.isKinematic = true; 
    }

    void FixedUpdate()
    {
        float t = Mathf.PingPong(Time.time * moveSpeed, 1f);
        Vector3 targetPos = Vector3.Lerp(startPos, startPos + moveOffset, t);
        
        if (rb != null)
            rb.MovePosition(targetPos);
        else
            transform.position = targetPos;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 判斷是否踩在頂部
            if (collision.contacts[0].normal.y < -0.5f) // 注意：這裡從平台角度看，法線向下表示物體在上方
            {
                // 建議：如果擔心縮放問題，可以建立一個空物體當作容器，或者手動計算位移差
                collision.transform.SetParent(transform);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            // 離開平台後，建議將玩家的 LocalScale 重設，或確保平台 Scale 永遠是 1
        }
    }
}