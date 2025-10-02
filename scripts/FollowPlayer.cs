using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public MouseSensitivityController sensitivityController;        
    public Vector3 offset = new Vector3(0, 2, -5);
    public float mouseSensitivity = 3f;     

    private float rotationX = 0f;   
    private float rotationY = 0f;
    public float MovementRotation { get; private set; }   // 用屬性取代 public 欄位

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityController.mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityController.mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -20f, 60f); 
        MovementRotation = rotationY;

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(target); 
    }

    // 提供角色存取相機的 Y 軸旋轉
    public float ReturnRotation()
    {
        return MovementRotation;
    }
}
