using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager instance;
    public GameObject fireballPrefab;
    public GameObject bigFireballPrefab;
    public GameObject frostGroundPrefab;
    public GameObject frostStormPrefab;
    public GameObject ligthningOrbPrefab;
    public GameObject divineThunderPrefab;
    public GameObject player;
    public GameObject playerFireballStart;
    public GameObject playerFrostGroundStart;

    public Spell fireballSpell;
    public Spell frostGround;
    public Spell lightningOrb;

    private float spellCooldown = 0;
    private float currentSpellDelay = 0;

    private float specialSpellCooldown = 0;
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

            if (Input.GetButton("Fire1") && currentSpellDelay >= spellCooldown)
            {
                HandleSpell(PlayerData.equippedSpell);
            }
            if (Input.GetButton("Fire2") && currentSpecialSpellDelay >= specialSpellCooldown)
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
            case SpellEnum.LightningOrb:
                CastLightningOrb();
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
            case SpellEnum.LightningOrb:
                CastDivineThunder();
                break;
            default:
                break;
        }
    }

    public void CastFireball()
    {
        spellCooldown = fireballSpell.baseCooldown;

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
        specialSpellCooldown = fireballSpell.baseSpecialCooldown;

        GameObject temp = Instantiate(bigFireballPrefab, playerFireballStart.transform.position, Quaternion.identity);
        //temp.transform.localScale *= 5;

        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - playerFireballStart.transform.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * fireballSpell.baseSpecialSpeed;

        temp.GetComponent<Damager>().damage = fireballSpell.baseSpecialDamage;
    }

    public void CastIceGround()
    {
        spellCooldown = frostGround.baseCooldown;

        GameObject temp = Instantiate(frostGroundPrefab, playerFrostGroundStart.transform.position, Quaternion.identity);
        temp.GetComponent<Damager>().damage = frostGround.baseDamage;
        temp.GetComponent<IceGround>().fadeDuration = frostGround.baseDuration;
    }

    public void CastIceStorm()
    {
        specialSpellCooldown = frostGround.baseSpecialCooldown;

        GameObject temp = Instantiate(frostStormPrefab, new Vector2(0, 0), Quaternion.identity);
        temp.GetComponent<Damager>().damage = frostGround.baseSpecialDamage;
        temp.GetComponent<IceStorm>().duration = frostGround.baseSpecialDuration;
        temp.GetComponent<IceStorm>().delayBetweenHits = frostGround.delayBetweenSpecialDamage;
    }

    public void CastLightningOrb()
    {
        spellCooldown = lightningOrb.baseCooldown;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject temp = Instantiate(ligthningOrbPrefab, mousePos, Quaternion.identity);
        temp.GetComponent<Damager>().damage = lightningOrb.baseDamage;
        temp.GetComponent<LightningOrb>().duration = lightningOrb.baseDuration;
        temp.GetComponent<LightningOrb>().delayBetweenHits = lightningOrb.delayBetweenDamage;
    }

    public void CastDivineThunder()
    {
        specialSpellCooldown = lightningOrb.baseSpecialCooldown;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject temp = Instantiate(divineThunderPrefab, mousePos, Quaternion.identity);
        temp.GetComponent<Damager>().damage = frostGround.baseDamage;
        temp.GetComponent<DivineThunder>().fadeDuration = lightningOrb.baseSpecialDuration;
    }
}
