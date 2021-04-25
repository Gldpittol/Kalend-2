using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCollision : MonoBehaviour
{
    public static CharacterCollision instance;

    public float delayBeforeDeath;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerTakeDamage(float damage)
    {
        PlayerData.currentHealth -= damage;
        print(PlayerData.currentHealth);
        if(PlayerData.currentHealth <= 0)
        {
            StartCoroutine(KillPlayer());
        }
    }

    private IEnumerator KillPlayer()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(delayBeforeDeath);

        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }
}
