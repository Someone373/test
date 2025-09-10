using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform spawnPoint; // 放金幣的位置

    private GameObject currentCoin;

    void Start()
    {
        SpawnCoin();
    }

    public void SpawnCoin()
    {
        currentCoin = Instantiate(coinPrefab, spawnPoint.position, Quaternion.Euler(90, 0, 0));
    }

    public void OnCoinCollected()
    {
        Invoke("SpawnCoin", 2f); // 兩秒後再生一顆
    }
}
