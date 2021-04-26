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

    public float spellCooldown = 0;
    public float currentSpellDelay = 0;

    public float specialSpellCooldown = 0;
    public float currentSpecialSpellDelay = 0;

    public List<GameObject> spellsActive = new List<GameObject>();
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            if (PlayerPrefs.HasKey("CurrentSpell")) PlayerData.equippedSpell = (SpellEnum)PlayerPrefs.GetInt(("CurrentSpell"));
            else PlayerData.equippedSpell = SpellEnum.None;
            if (PlayerPrefs.HasKey("MaxDepth")) PlayerData.maxDepth = PlayerPrefs.GetInt("MaxDepth");

            PlayerPrefs.Save();
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
            currentSpellDelay += Time.deltaTime * PlayerData.spellCooldownMultiplier;
            currentSpecialSpellDelay += Time.deltaTime * PlayerData.spellCooldownMultiplier;

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

        temp.GetComponent<Damager>().damage = fireballSpell.baseDamage * PlayerData.spellDamageMultiplier;

        spellsActive.Add(temp);
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

        temp.GetComponent<Damager>().damage = fireballSpell.baseSpecialDamage * PlayerData.spellDamageMultiplier;
        spellsActive.Add(temp);
    }

    public void CastIceGround()
    {
        spellCooldown = frostGround.baseCooldown;

        GameObject temp = Instantiate(frostGroundPrefab, playerFrostGroundStart.transform.position, Quaternion.identity);
        temp.GetComponent<Damager>().damage = frostGround.baseDamage * PlayerData.spellDamageMultiplier;
        temp.GetComponent<IceGround>().fadeDuration = frostGround.baseDuration;
        spellsActive.Add(temp);
    }

    public void CastIceStorm()
    {
        specialSpellCooldown = frostGround.baseSpecialCooldown;

        GameObject temp = Instantiate(frostStormPrefab, new Vector2(0, 0), Quaternion.identity);
        temp.GetComponent<Damager>().damage = frostGround.baseSpecialDamage * PlayerData.spellDamageMultiplier;
        temp.GetComponent<IceStorm>().duration = frostGround.baseSpecialDuration * PlayerData.spellDurationMultiplier;
        temp.GetComponent<IceStorm>().delayBetweenHits = frostGround.delayBetweenSpecialDamage;
        spellsActive.Add(temp);
    }

    public void CastLightningOrb()
    {
        spellCooldown = lightningOrb.baseCooldown;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject temp = Instantiate(ligthningOrbPrefab, mousePos, Quaternion.identity);
        temp.GetComponent<Damager>().damage = lightningOrb.baseDamage * PlayerData.spellDamageMultiplier;
        temp.GetComponent<LightningOrb>().duration = lightningOrb.baseDuration * PlayerData.spellDurationMultiplier;
        temp.GetComponent<LightningOrb>().delayBetweenHits = lightningOrb.delayBetweenDamage;
        spellsActive.Add(temp);
    }

    public void CastDivineThunder()
    {
        specialSpellCooldown = lightningOrb.baseSpecialCooldown;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject temp = Instantiate(divineThunderPrefab, mousePos, Quaternion.identity);
        temp.GetComponent<Damager>().damage = lightningOrb.baseSpecialDamage * PlayerData.spellDamageMultiplier;
        temp.GetComponent<DivineThunder>().fadeDuration = lightningOrb.baseSpecialDuration;
        spellsActive.Add(temp);
    }
}
