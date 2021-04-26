using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    public GameObject lobbyHole;

    public AudioClip fountainMusic;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayerData.currentDepth = 0;
        PlayerData.currentHealth = 100;
        PlayerData.invulnerabilityRemaining = 0;
        PlayerData.maxHealth = 100;
        PlayerData.movementSpeed = 1;
        PlayerData.spellCooldownMultiplier = 1;
        PlayerData.spellDamageMultiplier = 1;
        PlayerData.spellDurationMultiplier = 1;

        lobbyHole.GetComponent<HoleScript>().DecideIfOpen();

        if (MusicManager.instance.audSource.clip.name != fountainMusic.name)
        {
            MusicManager.instance.audSource.clip = fountainMusic;
            MusicManager.instance.audSource.Play();
            MusicManager.instance.audSource.volume = 0.5f;
        }
    }
}
