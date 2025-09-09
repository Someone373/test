using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DummyAnimator : MonoBehaviour
{
    private Animator animator;
        
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool isMoving = (h != 0 || v != 0);
        if (isMoving)
        {
            animator.SetTrigger("startWalk");
        }
        else if (!isMoving)
        {
            animator.SetTrigger("stopWalk");
        }
      
    }
}
