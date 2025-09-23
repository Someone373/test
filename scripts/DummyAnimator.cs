using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DummyAnimator : MonoBehaviour
{
    private Animator animator;
    private bool isGrounded = true;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0)
        {
            animator.SetTrigger("stopWalk");
        }

        animator.SetFloat("walkX", h);
        animator.SetFloat("walkZ", v);


        if (Input.GetKey(KeyCode.Space)&&isGrounded)
        {
            animator.SetTrigger("jump");
        }
        if (isGrounded==false) Debug.Log(isGrounded); 
                    
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsGrounded", true);

            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("IsGrounded",false);
        }
    }
}
