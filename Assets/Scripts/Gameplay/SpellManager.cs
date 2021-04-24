using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellEnum activeSpell = SpellEnum.Fireball;
    public static SpellManager instance;
    public GameObject fireballPrefab;
    public GameObject player;
    public GameObject playerFireballStart;

    public Spell fireballSpell;

    private float spellDelay = 0;
    private float currentSpellDelay = 0;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        currentSpellDelay += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && currentSpellDelay >= spellDelay)
        {
            HandleSpell(activeSpell);
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
}
