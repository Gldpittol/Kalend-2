using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAGoblin : MonoBehaviour
{
    public EnemyController enemyController;

    public Animator animator;

    public float distanceBeforeGoingBack;

    public float currentDelayBetweenHits = 0;

    public GameObject arrowPrefab;

    public GameObject arrowSpawnPos;

    private void Start()
    {
        float multiplier = PlayerData.currentDepth / 20 + 1;
        int realMultiplier = (int)multiplier;

        if (multiplier > 1)
        {
            enemyController.health *= realMultiplier;
            enemyController.baseDamage *= realMultiplier;
        }

        StartCoroutine(ShootArrow());
    }


    private void Update()
    {
        if(Vector2.Distance(transform.position, CharacterManager.instance.transform.position) < distanceBeforeGoingBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, CharacterManager.instance.gameObject.transform.position, -enemyController.speed * Time.deltaTime);
            animator.Play("GoblinRunBackwards");
        }

        else
        {
            animator.Play("GoblinIdle");
        }

        if (transform.position.x < CharacterManager.instance.gameObject.transform.position.x) transform.localScale = new Vector2(1, 1);
        else transform.localScale = new Vector2(-1, 1);
    }

    private IEnumerator ShootArrow()
    {
        yield return new WaitForSeconds(enemyController.delayBetweenAttacks);

        GameObject temp = Instantiate(arrowPrefab, arrowSpawnPos.transform.position, Quaternion.identity);
        temp.GetComponent<ArrowScript>().damage = enemyController.baseDamage;

        Vector3 shootDirection;
        shootDirection = CharacterManager.instance.transform.position;
        shootDirection = shootDirection - arrowSpawnPos.transform.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * enemyController.projectileSpeed;

        temp.transform.right = shootDirection;

        StartCoroutine(ShootArrow());
    }
}
