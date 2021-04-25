using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAttacks
{
    GiantSmash1,
    GiantSmash2,
    GiantPound
}

public class IABoss3 : MonoBehaviour
{
    public GameObject bossWall;
    public GameObject smashProjectilePrefab;
    public GameObject poundProjectilePrefab;
    public GameObject smashProjectileSpawnPos;
    public GameObject poundProjectileSpawnPos;

    public Animator animator;
    public float bossWallMinX, bossWallMaxX;
    public int bossWallDirection = 1;
    public float bossWallSpeed;

    public BossAttacks bossAttacks;

    public float delayBetweenAttacks;
    public float currentDelayBetweenAttacks;

    public float smashDamage;
    public float poundDamage;

    public float smashProjectileSpeed;
    public float poundProjectileSpeed;

    public int smashCount = 0;
    public int maxSmashCount;

    private void Start()
    {
        animator.Play("GiantIdle");
    }

    private void Update()
    {
        MoveBossWall();

        currentDelayBetweenAttacks += Time.deltaTime;

        if(currentDelayBetweenAttacks >= delayBetweenAttacks)
        {
            RunAI();
        }
    }

    private void RunAI()
    {
        currentDelayBetweenAttacks = 0;

        if(bossAttacks == BossAttacks.GiantSmash1)
        {
            smashCount = 0;
            animator.enabled = true;
            animator.Play("GiantSmash");
            bossAttacks = BossAttacks.GiantSmash2;
        }

        else if (bossAttacks == BossAttacks.GiantSmash2)
        {
            smashCount = 0;
            animator.enabled = true;
            animator.Play("GiantSmash");
            bossAttacks = BossAttacks.GiantPound;
        }

        else if (bossAttacks == BossAttacks.GiantPound)
        {
            animator.enabled = true;
            maxSmashCount++;
            animator.Play("GiantPound");
            bossAttacks = BossAttacks.GiantSmash1;
        }
    }

    private void PerformPound()
    {
        for (float i = bossWallMinX - 10; i < bossWallMaxX + 10; i += 1f)
        {
            GameObject temp = Instantiate(poundProjectilePrefab, new Vector2(i, poundProjectileSpawnPos.transform.position.y), Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * poundProjectileSpeed;
            temp.GetComponent<Boss2OrbScript>().damage = smashDamage;
            temp.GetComponent<Boss2OrbScript>().isBoss3Pound = true;
        }

        animator.Play("GiantPostPound");
    }

    private void PerformSmash()
    {
        currentDelayBetweenAttacks = 0;

        for(int i = -180; i < 180; i += 7)
        {
            GameObject temp = Instantiate(smashProjectilePrefab, smashProjectileSpawnPos.transform.position, Quaternion.Euler(0,0,i));
            temp.GetComponent<Boss2OrbScript>().damage = smashDamage;
            Rigidbody2D tempRb = temp.GetComponent<Rigidbody2D>();
            tempRb.velocity = temp.transform.right * smashProjectileSpeed;
        }

        smashCount++;

        if (smashCount >= maxSmashCount)
        {
            animator.Play("GiantIdle");
        }


    }

    private void MoveBossWall()
    {
        bossWall.transform.position = new Vector2(bossWall.transform.position.x + bossWallSpeed * Time.deltaTime, bossWall.transform.position.y);

        if (bossWall.transform.position.x > bossWallMaxX) bossWallSpeed = -Mathf.Abs(bossWallSpeed);
        else if (bossWall.transform.position.x < bossWallMinX) bossWallSpeed = Mathf.Abs(bossWallSpeed);
    }
}
