using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator animator;
    public bool isMoving = false;
    public bool isMovingLeft = false;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMovingLeft)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        animator.SetBool("IsMoving", isMoving);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    public void TriggerDashAnimation()
    {
        animator.SetTrigger("Dash");
        animator.SetBool("IsDashing", true);
    }

    public void StopDashAnimation()
    {
        animator.SetBool("IsDashing", false);
        animator.ResetTrigger("Dash");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        // Disable player movement
        transform.parent.GetComponent<PlayerMovement>().canMove = false;
    }
}
