using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum HoleBuff
{
    None,
    Health,
    Damage,
    CooldownReduction,
    MovementSpeed,
    SpellDuration,
    Invulnerability
}

public class HoleScript : MonoBehaviour
{
    public BoxCollider2D holeCollider;
    public HoleBuff holeBuff;
    public GameObject holeBuffImage;
    public GameObject trapDoorImage;

    public float healthAddition = 25f;
    public float spellCooldownMultiplier = 0.9f;
    public float spellDamageMultiplier = 1.1f;
    public float movementSpeed = 1.1f;
    public float invulnerabilityDuration = 15;
    public float spellDurationMultiplier = 1.1f;
    private void Update()
    {
        DecideIfOpen();
    }

    private void DecideIfOpen()
    {
        if (Spawner.instance && Spawner.instance.enemyHolder.transform.childCount == 0 && holeCollider.enabled == false)
        {
            holeCollider.enabled = true;
            holeBuffImage.SetActive(true);
            trapDoorImage.SetActive(false);
        }
        else if (!Spawner.instance)
        {
            holeCollider.enabled = true;
            trapDoorImage.SetActive(false);
        }
    }

    public void HoleBehaviour()
    {
        CharacterManager.instance.interactablesCollisionList.Clear();
        SpellManager.instance.currentSpecialSpellDelay = 0;
        SpellManager.instance.currentSpellDelay = 0;
        SpellManager.instance.spellCooldown = 0;
        SpellManager.instance.specialSpellCooldown = 0;
        SpellManager.instance.spellsActive.Clear();


        if (PlayerPrefs.HasKey("MaxDepth"))
        {
            PlayerData.currentDepth++;
            PlayerData.maxDepth = PlayerPrefs.GetInt("MaxDepth");

            if (PlayerData.currentDepth > PlayerData.maxDepth)
            {
                PlayerData.maxDepth = PlayerData.currentDepth;
                PlayerPrefs.SetInt("MaxDepth", PlayerData.maxDepth);
                print(PlayerPrefs.GetInt("MaxDepth"));
            }

            ApplyHoleBuff();
            SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
        }

        else 
        {
            PlayerData.currentDepth = 0;
            PlayerPrefs.SetInt("MaxDepth", 0);
            PlayerPrefs.SetInt("CanLoad", 1);
           
            SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        }
    }

    public void DecideHoleBuff()
    {
        holeBuff = (HoleBuff)Random.Range(1, 7);
    }

    public void ApplyHoleBuff()
    {
        switch(holeBuff)
        {
            case HoleBuff.CooldownReduction:
                PlayerData.spellCooldownMultiplier *= spellCooldownMultiplier;
                break;
            case HoleBuff.Damage:
                PlayerData.spellDamageMultiplier *= spellDamageMultiplier;
                break;
            case HoleBuff.SpellDuration:
                PlayerData.spellDurationMultiplier *= spellDurationMultiplier;
                break;
            case HoleBuff.Health:
                PlayerData.currentHealth += healthAddition;
                break;
            case HoleBuff.Invulnerability:
                PlayerData.invulnerabilityRemaining = invulnerabilityDuration;
                break;
            case HoleBuff.MovementSpeed:
                PlayerData.movementSpeed = movementSpeed;
                break;
        }
    }
}
