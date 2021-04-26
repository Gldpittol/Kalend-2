using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Gameplay,
    Cutscene,
    Menu, 
    Paused,
    GameOver
}

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static GameState gameState = GameState.Gameplay;

    private void Awake()
    {
        instance = this;
    }
}
