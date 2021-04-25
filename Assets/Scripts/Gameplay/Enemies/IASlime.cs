using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASlime : MonoBehaviour
{
    public EnemyController enemyController;
    public Animator animator;
    public BoxCollider2D slimeCollider;

    public bool canMove;
    public bool canSpeed;

    public Vector2 positionToMoveTo;

    public float currentDelay = 0;
    public bool isIdle;

    public AudioSource audSource;
    public AudioClip jumpClip;
    private void Update()
    {
        currentDelay += Time.deltaTime;
        if (currentDelay > enemyController.delayBetweenAttacks) EnableSpeed();

        if(canMove)
        {
            animator.Play("SlimeJump");
            if(canSpeed) transform.position = Vector2.MoveTowards(transform.position, positionToMoveTo, enemyController.speed * Time.deltaTime);
            currentDelay = 0;
        }
        else
        {
            animator.Play("SlimeIdle");
            positionToMoveTo = new Vector2(CharacterManager.instance.transform.position.x, CharacterManager.instance.transform.position.y - 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canMove && !canSpeed)
        {
            CharacterCollision.instance.PlayerTakeDamage(enemyController.baseDamage);
        }
    }

    public void PlayJumpClip()
    {
        audSource.PlayOneShot(jumpClip);
    }

    public void EnableSpeed()
    {
        canMove = true;
    }

    public void DisableSpeed()
    {
        canMove = false;
    }

    public void CanSpeed()
    {
        canSpeed = true;
    }

    public void CantSpeed()
    {
        canSpeed = false;
    }
    public void DisableBoxCollider()
    {
        slimeCollider.enabled = false;
    }

    public void enableBoxCollider()
    {
        slimeCollider.enabled = true;
    }
}
