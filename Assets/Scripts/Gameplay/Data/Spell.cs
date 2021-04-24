using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "New Spell")]
public class Spell : ScriptableObject
{
    public string spellName;
    public SpellEnum spell;
    public int depthToUnlock;
    public float baseDamage;
    public float baseCooldown;
    public float baseSpecialDamage;
    public float baseSpecialCooldown;
    public float baseSpeed;
    public float baseSpecialSpeed;
    public float baseDuration;
    public float baseSpecialDuration;
    public float delayBetweenDamage;
    public float delayBetweenSpecialDamage;
}
