using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainScript : MonoBehaviour
{
    public static FountainScript instance;

    public GameObject fountainPanel;
    public FountainButton[] fountainButtons;

    public AudioSource audSource;
    public AudioClip audClip;
    private void Awake()
    {
        instance = this;
    }
    public void OpenFountain()
    {
        audSource.PlayOneShot(audClip);

        GameController.gameState = GameState.Menu;
        fountainPanel.SetActive(true);

        foreach(FountainButton f in fountainButtons)
        {
            f.DecideIfInteractable();
        }
    }

    public void CloseFountain()
    {
        audSource.PlayOneShot(audClip);
        GameController.gameState = GameState.Gameplay;
        fountainPanel.SetActive(false);
    }
}
