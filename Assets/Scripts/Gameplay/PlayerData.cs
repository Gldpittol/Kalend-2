using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    [Header("Saved")]
    public static SpellEnum equippedSpell;
    public static int maxDepth= 22;
    public static bool seenDialogue0;
    public static bool seenDialogue1;
    public static bool seenDialogue2;

    [Header("Dynamic")]
    public static int currentDepth;
    public static float maxHealth = 100;
    public static float currentHealth = 100;
    public static float maxMana = 100;
    public static float currentMana = 100;
    public static float spellSpeedMultiplier = 1;
    public static float spellDamageMultiplier = 1;
}
