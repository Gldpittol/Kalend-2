using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager instance;
    public GameObject fireballPrefab;
    public GameObject player;
    public GameObject playerFireballStart;

    public Spell fireballSpell;

    private float spellDelay = 0;
    private float currentSpellDelay = 0;

    private float specialSpellDelay = 0;
    private float currentSpecialSpellDelay = 0;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            if (PlayerPrefs.HasKey("CurrentSpell")) PlayerData.equippedSpell = (SpellEnum)PlayerPrefs.GetInt(("CurrentSpell"));
            else PlayerData.equippedSpell = SpellEnum.Fireball;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (GameController.gameState == GameState.Gameplay)
        {
            currentSpellDelay += Time.deltaTime;
            currentSpecialSpellDelay += Time.deltaTime;

            if (Input.GetButton("Fire1") && currentSpellDelay >= spellDelay)
            {
                HandleSpell(PlayerData.equippedSpell);
            }
            if (Input.GetButton("Fire2") && currentSpecialSpellDelay >= specialSpellDelay)
            {
                HandleSpecialSpell(PlayerData.equippedSpell);
            }
        }
    }

    public void HandleSpell(SpellEnum activeSpell)
    {
        currentSpellDelay = 0;

        switch (activeSpell)
        {
            case SpellEnum.Fireball:
                CastFireball();
                break;
            case SpellEnum.FrostGround:
                CastIceGround();
                break;
            default:
                break;
        }
    }

    public void HandleSpecialSpell(SpellEnum activeSpell)
    {
        currentSpecialSpellDelay = 0;

        switch (activeSpell)
        {
            case SpellEnum.Fireball:
                CastSpecialFireball();
                break;
            case SpellEnum.FrostGround:
                CastIceStorm();
                break;
            default:
                break;
        }
    }

    public void CastFireball()
    {
        spellDelay = fireballSpell.baseCooldown;

        GameObject temp = Instantiate(fireballPrefab, playerFireballStart.transform.position, Quaternion.identity);

        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - playerFireballStart.transform.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized *fireballSpell.baseSpeed;

        temp.GetComponent<Damager>().damage = fireballSpell.baseDamage;
    }

    public void CastSpecialFireball()
    {
        specialSpellDelay = fireballSpell.baseSpecialCooldown;

        GameObject temp = Instantiate(fireballPrefab, playerFireballStart.transform.position, Quaternion.identity);
        temp.transform.localScale *= 5;

        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - playerFireballStart.transform.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * fireballSpell.baseSpecialSpeed;

        temp.GetComponent<Damager>().damage = fireballSpell.baseSpecialDamage;
    }

    public void CastIceGround()
    {

    }

    public void CastIceStorm()
    {

    }
}
