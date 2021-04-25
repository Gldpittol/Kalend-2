using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public int baseAmountToSpawn;

    public GameObject player;
    public GameObject holePrefab;
    public GameObject enemyHolder;

    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss3;


    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;

    public Vector2 playerPosLowerLeft;
    public Vector2 playerPosUpperRight;

    public Vector2 enemyPosLowerLeft;
    public Vector2 enemyPosUpperRight;

    public Vector2 hole1LowerLeft;
    public Vector2 hole1UpperRight;
    public Vector2 hole2LowerLeft;
    public Vector2 hole2UpperRight;
    public Vector2 hole3LowerLeft;
    public Vector2 hole3UpperRight;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Start()
    {
        SpawnPlayer();
        if (!CheckIfBossRoom(PlayerData.currentDepth))
        {
            SpawnEnemies(); 
        }
        else
        {
            SpawnBoss();
        }
        SpawnHoles();
    }

    private void SpawnBoss()
    {
        int room = PlayerData.currentDepth % 20;

        switch(room)
        {
            case 6:
                boss1.SetActive(true);
                boss1.transform.parent = enemyHolder.transform;
                break;
            case 13:
                boss2.SetActive(true);
                boss2.transform.parent = enemyHolder.transform;
                break;
            case 0:
                if (PlayerData.currentDepth > 0)
                {
                    boss3.SetActive(true);
                    boss3.transform.parent = enemyHolder.transform;
                }

                break;
            default:
                break;
        }
    }

    public static bool CheckIfBossRoom(int depth)
    {
        int room = depth % 20;

        return (room == 6 || room == 13 || room == 0) && depth != 0;
    }
    private void SpawnEnemies()
    {
        for(int i = 0; i < (PlayerData.currentDepth - 1 + 3); i++)
        {
            float enemyX = Random.Range(enemyPosLowerLeft.x, enemyPosUpperRight.x);
            float enemyY = Random.Range(enemyPosLowerLeft.y, enemyPosUpperRight.y);

            int highestEnemyIndexToSpawn = 0;

            if (PlayerData.currentDepth < 2) highestEnemyIndexToSpawn = 0;

            else if (PlayerData.currentDepth < 5) highestEnemyIndexToSpawn = 2;

            else if (PlayerData.currentDepth < 8) highestEnemyIndexToSpawn = 3;

            else if (PlayerData.currentDepth >= 8) highestEnemyIndexToSpawn = 4;

            Instantiate(enemyPrefabs[Random.Range(0, highestEnemyIndexToSpawn)], new Vector2(enemyX, enemyY), Quaternion.identity, enemyHolder.transform);
        }   
    }

    private void SpawnHoles()
    {
        float hole1X = Random.Range(hole1LowerLeft.x, hole1UpperRight.x);
        float hole1Y = Random.Range(hole1LowerLeft.y, hole1UpperRight.y);
        GameObject temp = Instantiate(holePrefab, new Vector2(hole1X, hole1Y), Quaternion.identity);
        temp.GetComponent<HoleScript>().DecideHoleBuff();
        temp.GetComponent<HoleScript>().canPlaySound = true;

        float hole2X = Random.Range(hole2LowerLeft.x, hole2UpperRight.x);
        float hole2Y = Random.Range(hole2LowerLeft.y, hole2UpperRight.y);
        temp = Instantiate(holePrefab, new Vector2(hole2X, hole2Y), Quaternion.identity);
        temp.GetComponent<HoleScript>().DecideHoleBuff();

        float hole3X = Random.Range(hole3LowerLeft.x, hole3UpperRight.x);
        float hole3Y = Random.Range(hole3LowerLeft.y, hole3UpperRight.y);
        temp = Instantiate(holePrefab, new Vector2(hole3X, hole3Y), Quaternion.identity);
        temp.GetComponent<HoleScript>().DecideHoleBuff();
    }

    private void SpawnPlayer()
    {
        player = CharacterManager.instance.gameObject;

        float playerX = Random.Range(playerPosLowerLeft.x, playerPosUpperRight.x);
        float playerY = Random.Range(playerPosLowerLeft.y, playerPosUpperRight.y);
        player.transform.position = new Vector2(playerX, playerY);
    }
}
