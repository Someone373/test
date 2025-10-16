using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 0.3f;      // 玩家踩上後幾秒掉下
    public float respawnDelay = 3f;   // 掉下後幾秒重生
    private float gravity = -1.5f;
    public float slopeLimit = 35f;
    public float slideSpeed = 5f;

    private Vector3 originalPosition; // 初始位置
    private Quaternion originalRotation;
    private Rigidbody rb;

    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        rb.useGravity = false;
        rb.isKinematic = true; // 一開始不會掉
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        rb.AddForce(0,gravity,0);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAfterDelay());
        }
    }

    IEnumerator FallAfterDelay()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);

        rb.isKinematic = false; // 掉下去
        yield return new WaitForSeconds(respawnDelay);

        // 重生：回到原位並凍結
        rb.isKinematic = true;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        isFalling = false;
    }
}
