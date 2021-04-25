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
    public float bossWallMinX, bossWallMaxX;
    public int bossWallDirection = 1;
    public float bossWallSpeed;

    public BossAttacks bossAttacks;

    public float delayBetweenAttacks;
    public float currentDelayBetweenAttacks;


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

        }
    }

    private void MoveBossWall()
    {
        bossWall.transform.position = new Vector2(bossWall.transform.position.x + bossWallSpeed * Time.deltaTime, bossWall.transform.position.y);

        if (bossWall.transform.position.x > bossWallMaxX) bossWallSpeed = -Mathf.Abs(bossWallSpeed);
        else if (bossWall.transform.position.x < bossWallMinX) bossWallSpeed = Mathf.Abs(bossWallSpeed);
    }
}
