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

    private void Update()
    {
        if (instance == null) instance = this;

        if(SceneManager.GetActiveScene().name != "Corridor") PlayerData.invulnerabilityRemaining -= Time.deltaTime;
    }

    public void PlayerTakeDamage(float damage)
    {
        if (PlayerData.invulnerabilityRemaining > 0)
        {
            print("Invulnerable");
            return;
        }
        
        PlayerData.currentHealth -= damage;
        if(PlayerData.currentHealth <= 0)
        {
            StartCoroutine(KillPlayer());
        }
    }

    private IEnumerator KillPlayer()
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        yield return null;
    }
}
