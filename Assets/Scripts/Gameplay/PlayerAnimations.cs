using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string IdleAnim = "PlayerIdle";
    private const string RunAnim = "PlayerRun";
    private const string RunAnimBackwards = "PlayerRunBackwards";

    public Animator animator;

    private void Start()
    {
        animator.Play(IdleAnim);
    }
    void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        if ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0))
        {
            if ((Input.GetAxisRaw("Horizontal") < 0) && (transform.localScale.x > 0)) animator.Play(RunAnimBackwards);
            else if ((Input.GetAxisRaw("Horizontal") > 0) && (transform.localScale.x < 0)) animator.Play(RunAnimBackwards);
            else animator.Play(RunAnim);
        }

        if ((Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0))
        {
            animator.Play(IdleAnim);
        }
    }
}
