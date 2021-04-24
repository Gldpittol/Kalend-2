using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    [Header("Saved")]
    public static SpellEnum equippedSpell;
    public static int maxDepth;
    public static bool seenDialogue0;
    public static bool seenDialogue1;
    public static bool seenDialogue2;

    [Header("Dynamic")]
    public static int currentDepth;
    public static float maxHealth = 100;
    public static float currentHealth = 100;
    public static float spellCooldownMultiplier = 1;
    public static float spellDamageMultiplier = 1;
    public static float movementSpeed = 1;
    public static float invulnerabilityRemaining = 0;
    public static float spellDurationMultiplier = 1;
}
