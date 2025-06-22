using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;             // 角色物件
    public Transform cameraTransform;    // 攝影機本體
    public float distance = 5f;
    public float height = 2f;
    public float mouseSensitivity = 3f;
    public float minY = -35f;
    public float maxY = 60f;
    public LayerMask collisionMask;
    private float rotX = 0f;
    private float rotY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        rotY += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotX = Mathf.Clamp(rotX, minY, maxY);

        // 移動到角色頭上位置
        transform.position = target.position + Vector3.up * height;

        // 根據滑鼠旋轉 pivot
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);

        Vector3 desiredCameraPos = transform.position - transform.forward * distance;

        // 防止攝影機穿牆
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit, distance, collisionMask))
        {
            cameraTransform.position = hit.point + transform.forward * 0.2f;
        }
        else
        {
            cameraTransform.position = desiredCameraPos;
        }

        cameraTransform.LookAt(transform.position);
    }
}
