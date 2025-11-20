using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerKnockback : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 knockbackVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 如果正在被擊退
        if (knockbackVelocity.magnitude > 0.1f)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);

            // 逐漸減速
            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, 5f * Time.deltaTime);
        }
    }

    // 被石頭呼叫的函式
    public void Knockback(Vector3 direction, float force, float duration)
    {
        knockbackVelocity = direction.normalized * force;
    }
}
