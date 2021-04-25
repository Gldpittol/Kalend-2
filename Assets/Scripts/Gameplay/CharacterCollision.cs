using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCollision : MonoBehaviour
{
    public static CharacterCollision instance;

    public float delayBeforeDeath;

    public Color hitColor;

    public AudioClip audClip;
    public AudioSource audSource;

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
            return;
        }
        
        PlayerData.currentHealth -= damage;
        StartCoroutine(ChangeColor());
        audSource.PlayOneShot(audClip);

        if (PlayerData.currentHealth <= 0)
        {
            StartCoroutine(KillPlayer());
        }
    }

    private IEnumerator KillPlayer()
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        yield return null;
    }

    public IEnumerator ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = hitColor;
        yield return new WaitForSeconds(0.1f); 
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
