using UnityEngine;

public class Boulder : MonoBehaviour
{
    public float hitForce = 8f;         // 撞擊力量
    public float knockbackTime = 0.7f;  // 撞擊持續時間
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 取得角色的 PlayerKnockback 腳本
            PlayerKnockback kb = collision.gameObject.GetComponent<PlayerKnockback>();
            if (kb != null)
            {
                // 計算方向（從石頭 → 玩家）
                Vector3 dir = (collision.transform.position - transform.position).normalized;
                dir.y = 0.2f; // 稍微往上
                kb.Knockback(dir, hitForce, knockbackTime);
            }
        }
    }
}
