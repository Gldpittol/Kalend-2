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
    public void OnSpellSwap()
    {
        PlayerData.equippedSpell = spell.spell;
        PlayerPrefs.SetInt("CurrentSpell", (int)PlayerData.equippedSpell);
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
