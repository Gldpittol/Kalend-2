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
    public Animator animator;
    public HoleBuff holeBuff;
    public GameObject holeBuffImage;
    public GameObject transitionPrefab;

    public float healthAddition = 25f;
    public float spellCooldownMultiplier = 0.9f;
    public float spellDamageMultiplier = 1.1f;
    public float movementSpeed = 1.1f;
    public float invulnerabilityDuration = 15;
    public float spellDurationMultiplier = 1.1f;

    public Sprite buffHealth;
    public Sprite buffDamage;
    public Sprite buffMov;
    public Sprite buffCD;
    public Sprite buffInvuln;
    public Sprite buffSpellDuration;

    public AudioSource audSource;
    public AudioClip audClip;

    public bool canPlaySound;

    private void Update()
    {
        DecideIfOpen();
    }

    public void DecideIfOpen()
    { 
        if (Spawner.instance && Spawner.instance.enemyHolder.transform.childCount == 0 && holeCollider.enabled == false && !animator.enabled)
        {
            animator.enabled = true;
            if (canPlaySound)
            {
                audSource.PlayOneShot(audClip);
            }

            CharacterManager.instance.interactablesCollisionList.Clear();


            if (Spawner.instance.orbHolder) Destroy(Spawner.instance.orbHolder);

        }
        else if (!Spawner.instance && !animator.enabled && PlayerData.equippedSpell != SpellEnum.None)
        {
            animator.enabled = true;
            audSource.PlayOneShot(audClip);
            holeCollider.enabled = true;
            holeBuffImage.SetActive(false);
        }

        else if(SceneManager.GetActiveScene().name == "Corridor" && !animator.enabled)
        {
            animator.enabled = true;
            audSource.PlayOneShot(audClip);
            holeCollider.enabled = true;
            holeBuffImage.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "Lobby" && PlayerData.equippedSpell == SpellEnum.None)
        {
            holeCollider.enabled = false;
            animator.enabled = false;
            holeBuffImage.SetActive(false);
        }
    }

    public void ActivateHoleBuff()
    {
        if (Spawner.instance && holeBuff != HoleBuff.None)
        {
            holeBuffImage.SetActive(true);
        }
        holeCollider.enabled = true;
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

            if (PlayerData.currentDepth > PlayerData.maxDepth && SceneManager.GetActiveScene().name != "Corridor")
            {
                PlayerData.maxDepth = PlayerData.currentDepth;
                PlayerPrefs.SetInt("MaxDepth", PlayerData.maxDepth);
            }

            if (PlayerData.maxDepth == 21 && !PlayerPrefs.HasKey("Dialogue5"))
            {
                StartCoroutine(GoToNewScene("Corridor"));
                return;
            }

            ApplyHoleBuff();
            if (!Spawner.CheckIfBossRoom(PlayerData.currentDepth))
            {
                if (SceneManager.GetActiveScene().name == "Corridor") PlayerData.currentDepth--;
                StartCoroutine(GoToNewScene("Dungeon"));
            }
            else
            {
                StartCoroutine(GoToNewScene("Corridor"));
            }
        }

        else 
        {
            PlayerData.currentDepth = 0;
            PlayerPrefs.SetInt("MaxDepth", 0);
            PlayerPrefs.SetInt("CanLoad", 1);

            StartCoroutine(GoToNewScene("Lobby"));
        }
    }

    public void DecideHoleBuff()
    {
        holeBuff = (HoleBuff)Random.Range(1, 7);

        switch (holeBuff)
        {
            case HoleBuff.CooldownReduction:
                holeBuffImage.GetComponent<SpriteRenderer>().sprite = buffCD;
                break;
            case HoleBuff.Damage:
                holeBuffImage.GetComponent<SpriteRenderer>().sprite = buffDamage;
                break;
            case HoleBuff.SpellDuration:
                holeBuffImage.GetComponent<SpriteRenderer>().sprite = buffSpellDuration;
                break;
            case HoleBuff.Health:
                holeBuffImage.GetComponent<SpriteRenderer>().sprite = buffHealth;
                break;
            case HoleBuff.Invulnerability:
                holeBuffImage.GetComponent<SpriteRenderer>().sprite = buffInvuln;
                break;
            case HoleBuff.MovementSpeed:
                holeBuffImage.GetComponent<SpriteRenderer>().sprite = buffMov;
                break;
            case HoleBuff.None:
                holeBuffImage.GetComponent<SpriteRenderer>().enabled = false;
                break;
        }
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
            case HoleBuff.None:
                break;
        }
    }

    public IEnumerator GoToNewScene(string sceneName)
    {
        Instantiate(transitionPrefab, CharacterManager.instance.transform.position, Quaternion.identity);

        while (!TransitionIn.transitionDone) yield return null;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
