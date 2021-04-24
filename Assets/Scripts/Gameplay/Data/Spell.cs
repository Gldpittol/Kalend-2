using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "New Spell")]
public class Spell : ScriptableObject
{
    public string spellName;
    public float baseDamage;
    public float baseCooldown;
    public float baseSpeed;
    public float baseManaCost;
}
