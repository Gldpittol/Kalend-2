using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IARock : MonoBehaviour
{
    public EnemyController enemyController;
    public Animator animator;
    private void Start()
    {
        StartCoroutine(RockIA());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterCollision.instance.PlayerTakeDamage(enemyController.baseDamage);
        }
    }

    public IEnumerator RockIA()
    {
        animator.Play("RockIdle");
        yield return new WaitForSeconds(enemyController.delayBetweenAttacks);
        animator.Play("RockRun");

        Vector2 positionToMoveTo = CharacterManager.instance.transform.position;

        while(Vector2.Distance(positionToMoveTo, transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, positionToMoveTo, enemyController.speed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(RockIA());
    }
}
