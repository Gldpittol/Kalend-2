using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialoguePanel;
    public Text dialogueText;

    public int maxIndex;
    public int currentIndex = 0;

    public string[] _dialogue;
    public int _ID;

    public GameObject playerStartPos;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CharacterManager.instance.transform.position = playerStartPos.transform.position;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(dialoguePanel.gameObject.activeInHierarchy)
            {
                if(currentIndex < maxIndex)
                {
                    dialogueText.text = _dialogue[currentIndex];
                    currentIndex++;
                }
                else
                {
                    dialoguePanel.SetActive(false);
                    StartCoroutine(SetPrefs());
                }
            }
        }
    }

    public void PlayDialogue(string[] dialogue, int ID)
    {
        GameController.gameState = GameState.Cutscene;
        maxIndex = dialogue.Length;
        currentIndex = 0;
        _dialogue = dialogue;
        _ID = ID;

        dialoguePanel.SetActive(true);
        dialogueText.text = _dialogue[currentIndex];
        currentIndex++;
    }

    public IEnumerator SetPrefs()
    {
        yield return new WaitForSeconds(1f);

        switch(_ID)
        {
            case 0:
                PlayerPrefs.SetInt("Dialogue1",1);
                break;
            case 1:
                PlayerPrefs.SetInt("Dialogue2", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("Dialogue3", 1);
                break;
            case 3:
                PlayerPrefs.SetInt("Dialogue4", 1);
                break;
        }

        PlayerPrefs.Save();
        GameController.gameState = GameState.Gameplay;
    }


}
