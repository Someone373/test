using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    public GameObject boulderPrefab;   // 石頭預製物件
    public float spawnInterval = 10f;   // 每幾秒生成一次
    public float launchForce = 500f;   // 初始推力
    public float lifeTime = 10f;       // 石頭存在多久後自動消失

    void Start()
    {
        InvokeRepeating(nameof(SpawnBoulder), 2f, spawnInterval);
    }

    void SpawnBoulder()
    {
        // 生成石頭
        GameObject boulder = Instantiate(boulderPrefab, transform.position, transform.rotation);

        // 給它初速度（朝著生成器面向方向）
        Rigidbody rb = boulder.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * launchForce);
        }

        // 自動銷毀石頭以免太多
        Destroy(boulder, lifeTime);
    }
}
