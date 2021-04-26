using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsList : MonoBehaviour
{
    public AudioSource audSource;
    public AudioClip audClip;

    public GameObject buffPanel;

    public void OpenBuffPanel()
    {
        audSource.PlayOneShot(audClip);

        GameController.gameState = GameState.Menu;
        buffPanel.SetActive(true);
    }

    public void CloseBuffScroll()
    {
        audSource.PlayOneShot(audClip);
        GameController.gameState = GameState.Gameplay;
        buffPanel.SetActive(false);
    }
}
