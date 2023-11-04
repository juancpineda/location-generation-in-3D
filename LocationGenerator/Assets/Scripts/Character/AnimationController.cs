//----------------------------------------------------------------------------------------------------------------------------------------
// The AnimationController class is a script that plays an animation if it detects that a transform has moved and stops the animation if it is still.
//----------------------------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public Transform target;
    public float threshold = 0.01f;

    private Vector3 previousPosition;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Target").transform;
        }

        previousPosition = target.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, previousPosition);

        if (distance > threshold)
        {
            animator.speed = 1;
            animator.Play("Move");
        }
        else
        {
            animator.speed = 0;
        }

        previousPosition = target.position;
    }
}