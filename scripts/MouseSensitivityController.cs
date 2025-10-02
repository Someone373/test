using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityController : MonoBehaviour
{
    public Scrollbar sensitivityScrollbar; // 滑鼠靈敏度滑條
    public float minSensitivity = 0.1f;    // 最小靈敏度
    public float maxSensitivity = 5f;      // 最大靈敏度

    [HideInInspector]
    public float mouseSensitivity = 1f;    // 真正的滑鼠靈敏度

    void Start()
    {
        if (sensitivityScrollbar != null)
        {
            // 初始化滑條位置（0~1）
            sensitivityScrollbar.value = 0.5f; 

            // 註冊滑條值變動事件
            sensitivityScrollbar.onValueChanged.AddListener(OnSensitivityChanged);

            // 初始化靈敏度
            UpdateSensitivity(sensitivityScrollbar.value);
        }
    }

    void OnSensitivityChanged(float value)
    {
        UpdateSensitivity(value);
    }

    void UpdateSensitivity(float value)
    {
        // 將滑條值（0~1）轉換為靈敏度範圍
        mouseSensitivity = Mathf.Lerp(minSensitivity, maxSensitivity, value);
        Debug.Log("滑鼠靈敏度: " + mouseSensitivity);
    }
}
