using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;        
    public Vector3 offset = new Vector3(0, 2, -5);
    public float mouseSensitivity = 3f;
    public float distance = 4f;     

    private float rotationX = 0f;   
    private float rotationY = 0f;    

    void Start()
    {
       
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
     
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -35f, 60f); 

        
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(target); 
        target.rotation = Quaternion.Euler(0, rotationY, 0);

    }
}
