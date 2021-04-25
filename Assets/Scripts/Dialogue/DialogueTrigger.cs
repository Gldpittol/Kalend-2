using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public struct Dialogues
    {
        [TextArea(10, 10)]
        public string[] dialogue;
    }

    public Dialogues[] dialogues;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))        
            DecideDialogue();
    }

    private void DecideDialogue()
    {
        switch(PlayerData.currentDepth)
        {
            case 0:
                if (!PlayerPrefs.HasKey("Dialogue1")) DialogueManager.instance.PlayDialogue(dialogues[0].dialogue, 0);
                break;
            case 6:
                if (!PlayerPrefs.HasKey("Dialogue2")) DialogueManager.instance.PlayDialogue(dialogues[1].dialogue, 1);
                break;
            case 13:
                if (!PlayerPrefs.HasKey("Dialogue3")) DialogueManager.instance.PlayDialogue(dialogues[2].dialogue, 2);
                break;
            case 20:
                if (!PlayerPrefs.HasKey("Dialogue4")) DialogueManager.instance.PlayDialogue(dialogues[3].dialogue, 3);
                break;
            case 21:
                if (!PlayerPrefs.HasKey("Dialogue5")) DialogueManager.instance.PlayDialogue(dialogues[4].dialogue, 4);
                break;
            default: 
                break;
        }

        this.gameObject.SetActive(false);

    }
}
