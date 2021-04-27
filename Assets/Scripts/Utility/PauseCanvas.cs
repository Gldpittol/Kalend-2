using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvas : MonoBehaviour
{
    public GameObject pausePanel;

    public GameObject transitionIn;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(PlayerPrefs.HasKey("MaxDepth")) StartCoroutine(PauseCoroutine());          
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

    public void ContinueGame()
    {
        GameController.gameState = GameState.Gameplay;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void ReturnToLobby()
    {
        StartCoroutine(GoToNewScene("Lobby"));
        Time.timeScale = 1f;
        PlayerData.invulnerabilityRemaining = 1f;
    }


    public IEnumerator GoToNewScene(string sceneName)
    {
        Instantiate(transitionIn, CharacterManager.instance.transform.position, Quaternion.identity);

        while (!TransitionIn.transitionDone) yield return null;

        TransitionIn.transitionDone = false;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

}
