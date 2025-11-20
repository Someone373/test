using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationAngle = 90f;     // 每次旋轉角度
    public float rotationSpeed = 180f;    // 每秒旋轉角度（控制旋轉速度）

    private bool isRotating = false;
    private Quaternion targetRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            // 設定目標旋轉角度
            targetRotation = transform.rotation * Quaternion.Euler(0, rotationAngle, 0);
            isRotating = true;
        }

        // 平滑旋轉到目標角度
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // 如果接近目標角度就停止
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }

    }

}
