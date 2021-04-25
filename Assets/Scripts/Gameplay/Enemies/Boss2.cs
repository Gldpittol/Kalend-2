using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public EnemyController enemyController;
    public Animator animator;
    public GameObject orbSpawn;
    public GameObject orbSpawnPos;
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
        animator.speed = 1f;
        yield return new WaitForSeconds(enemyController.delayBetweenAttacks);
        animator.speed = 2f;

        Vector2 positionToMoveTo = CharacterManager.instance.transform.position;

        while (Vector2.Distance(positionToMoveTo, transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, positionToMoveTo, enemyController.speed * Time.deltaTime);
            yield return null;
        }

        SpawnOrbs();
        StartCoroutine(RockIA());
    }

    public void SpawnOrbs()
    {
        StartCoroutine(SpawnOrbsCoroutine());
    }

    public IEnumerator SpawnOrbsCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnAnOrb(15);
            SpawnAnOrb(0);
            SpawnAnOrb(-15);
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    public void SpawnAnOrb(float offset)
    {
        GameObject temp = Instantiate(orbSpawn, orbSpawnPos.transform.position, Quaternion.identity);
        temp.GetComponent<Boss2OrbScript>().damage = enemyController.baseDamage;

        Vector3 shootDirection;
        shootDirection = CharacterManager.instance.gameObject.transform.position;
        shootDirection = shootDirection - orbSpawnPos.transform.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x + offset, shootDirection.y).normalized * enemyController.projectileSpeed;
    }

}
