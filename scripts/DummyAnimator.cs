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

        if (Mathf.Abs(h) < 0.1f) h = 0f;
        if (Mathf.Abs(v) < 0.1f) v = 0f;

        if (h == 0 && v == 0)
        {
            animator.SetTrigger("stopWalk");
        }

            animator.SetFloat("walkX", h);        
            animator.SetFloat("walkZ", v);
                      
    }
}
