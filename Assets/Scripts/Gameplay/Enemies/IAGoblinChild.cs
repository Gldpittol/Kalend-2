using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAGoblinChild : MonoBehaviour
{
    public EnemyController enemyController;

    public Animator animator;

    public bool canMove = true;

    public float currentDelayBetweenHits = 0;

    public bool isDamagingPlayer = false;

    private void Start()
    {
        enemyController.speed = Random.Range(enemyController.speed - 0.5f, enemyController.speed + 0.5f);
    }

    private void Update()
    {
        if(canMove) transform.position = Vector2.MoveTowards(transform.position, CharacterManager.instance.gameObject.transform.position, enemyController.speed * Time.deltaTime);
        if (transform.position.x < CharacterManager.instance.gameObject.transform.position.x) transform.localScale = new Vector2(1, 1);
        else transform.localScale = new Vector2(-1, 1);

        if (isDamagingPlayer) currentDelayBetweenHits += Time.deltaTime;

        if (currentDelayBetweenHits > enemyController.delayBetweenAttacks)
        {
            CharacterCollision.instance.PlayerTakeDamage(enemyController.baseDamage);
            currentDelayBetweenHits = 0;
        }

        if (canMove)
        {
            animator.Play("GoblinChild");
        }
        else
        {
            animator.Play("GoblinChildIdle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            currentDelayBetweenHits += Time.deltaTime;
            canMove = false;
            isDamagingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentDelayBetweenHits = 0; 
            isDamagingPlayer = false;
            canMove = true;
        }
    }


}
