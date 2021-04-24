using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Gameplay,
    Cutscene,
    Menu
}

public class GameController : MonoBehaviour
{
    public static GameState gameState = GameState.Gameplay;
}
