using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    public GameObject pausePanel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(PauseCoroutine());          
        }
    }

    public IEnumerator PauseCoroutine()
    {
        if (!pausePanel.activeInHierarchy && GameController.gameState == GameState.Gameplay)
        {
            GameController.gameState = GameState.Paused;
            yield return new WaitForSeconds(0.1f);
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }

        else if (pausePanel.activeInHierarchy && GameController.gameState == GameState.Paused)
        {
            GameController.gameState = GameState.Gameplay;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
    }
}
