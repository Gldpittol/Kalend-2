using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    public Text healthText;
    public Text depthText;
    public Text maxDepth;
    public Text equippedSpell;
    public Text spellDMGMult;
    public Text spellCDMult;
    public Text moveSpeedMult;
    public Text invulnerabilityTime;
    public Text spellDurationMultiplier;

    private void Start()
    {
        if (!PlayerData.debugHUD) this.gameObject.SetActive(false);
    }

    void Update()
    {
        healthText.text = "Health: " + PlayerData.currentHealth.ToString("F0");
        depthText.text = "Depth: " + PlayerData.currentDepth.ToString();
        maxDepth.text = "Max Depth " + PlayerData.maxDepth.ToString();
        equippedSpell.text = "Equipped Spell " + PlayerData.equippedSpell.ToString();
        spellDMGMult.text = "Spell Damage Mult " + PlayerData.spellDamageMultiplier.ToString();
        spellCDMult.text = "Spell Cd Mult " + PlayerData.spellCooldownMultiplier.ToString();
        moveSpeedMult.text = "Move Speed Mult " + PlayerData.movementSpeed.ToString();
        invulnerabilityTime.text = "Invulnerability Time " + PlayerData.invulnerabilityRemaining.ToString();
        spellDurationMultiplier.text = "Spell Duration Mult " + PlayerData.spellDurationMultiplier.ToString();
    }
}
