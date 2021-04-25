using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FountainButton : MonoBehaviour
{
    public Spell spell;
    public Text titleText;
    public Text descriptionTextText;
    public Text spellLockedText;
    public Image spellImage;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MaxDepth")) PlayerData.maxDepth = PlayerPrefs.GetInt("MaxDepth");
    }
    public void OnSpellSwap()
    {
        LobbyManager.instance.lobbyHole.GetComponent<HoleScript>().DecideIfOpen();

        foreach(GameObject g in SpellManager.instance.spellsActive)
        {
            if(g) Destroy(g.gameObject);
        }

        SpellManager.instance.spellsActive.Clear();
        SpellManager.instance.spellCooldown = 0;
        SpellManager.instance.specialSpellCooldown = 0;
        SpellManager.instance.currentSpecialSpellDelay = 0;
        SpellManager.instance.currentSpellDelay = 0;

        PlayerData.equippedSpell = spell.spell;
        PlayerPrefs.SetInt("CurrentSpell", (int)PlayerData.equippedSpell);
        PlayerPrefs.Save();
        FountainScript.instance.OpenFountain();
    }

    public void DecideIfInteractable()
    {
        if(PlayerData.equippedSpell == spell.spell)
        {
            GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }

        if (PlayerData.maxDepth >= spell.depthToUnlock)
        {
            GetComponent<Button>().interactable = true;
            spellLockedText.gameObject.SetActive(false);
            descriptionTextText.gameObject.SetActive(true);
            titleText.gameObject.SetActive(true);
            spellImage.gameObject.SetActive(true);
        }

        else
        {
            GetComponent<Button>().interactable = false;
            spellLockedText.gameObject.SetActive(true);
            descriptionTextText.gameObject.SetActive(false);
            titleText.gameObject.SetActive(false);
            spellLockedText.text = "Unlocks when you first reach Depth " + spell.depthToUnlock;
            spellImage.gameObject.SetActive(false);
        }
    }
}
