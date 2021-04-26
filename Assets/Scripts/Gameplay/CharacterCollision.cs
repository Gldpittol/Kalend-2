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

    public float soundCooldown = 1f;
    public float currentSoundCooldown = 1f;

    public GameObject shield;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        currentSoundCooldown += Time.deltaTime;

        if (instance == null) instance = this;

        if(SceneManager.GetActiveScene().name != "Corridor") PlayerData.invulnerabilityRemaining -= Time.deltaTime;

        if (PlayerData.invulnerabilityRemaining > 0 && !shield.activeInHierarchy) shield.SetActive(true);
        if (PlayerData.invulnerabilityRemaining <= 0 && shield.activeInHierarchy) shield.SetActive(false);

    }

    public void PlayerTakeDamage(float damage)
    {
        if (PlayerData.invulnerabilityRemaining > 0)
        {
            return;
        }
        
        PlayerData.currentHealth -= damage;
        StartCoroutine(ChangeColor());
        
        if(currentSoundCooldown > soundCooldown)
        {
            audSource.PlayOneShot(audClip);
            currentSoundCooldown = 0;
        }

        if (PlayerData.currentHealth < 0) PlayerData.currentHealth = 0;

        if (PlayerData.currentHealth < 1)
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
