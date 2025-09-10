using UnityEngine;

public class collect_coin : MonoBehaviour
{
    public float speed = 30f;

    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 通知 CoinManager 有人吃到金幣
            GameObject.FindObjectOfType<CoinManager>().OnCoinCollected();

            Destroy(gameObject); // 把金幣刪掉
        }
    }
}
