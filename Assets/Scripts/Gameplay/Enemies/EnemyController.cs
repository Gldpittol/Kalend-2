using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float baseDamage;
    public float health;
    public float speed;
    public float delayBetweenAttacks;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Destroy(this.gameObject);
    }
}
