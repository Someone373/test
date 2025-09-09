using UnityEngine;
using System.Collections;

public class BlinkingPlatform : MonoBehaviour
{
    public float visibleTime = 2f;   // 顯示多久
    public float invisibleTime = 2f; // 消失多久

    private Renderer platformRenderer;
    private Collider platformCollider;

    void Start()
    {
        // 取得外觀與碰撞器
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();

        // 啟動協程
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            // 顯示平台
            platformRenderer.enabled = true;
            platformCollider.enabled = true;
            yield return new WaitForSeconds(visibleTime);

            // 隱藏平台
            platformRenderer.enabled = false;
            platformCollider.enabled = false;
            yield return new WaitForSeconds(invisibleTime);
        }
    }
}
