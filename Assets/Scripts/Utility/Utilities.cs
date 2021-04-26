using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Utilities : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject loadButton;

    public GameObject transitionPrefab;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CanLoad")) loadButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        StartCoroutine(GoToNewScene("Corridor"));
    }

    public void LoadGame()
    {
        StartCoroutine(GoToNewScene("Lobby"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        optionsCanvas.SetActive(!optionsCanvas.activeInHierarchy);
    }

    public IEnumerator GoToNewScene(string sceneName)
    {
        Instantiate(transitionPrefab, transform.position, Quaternion.identity);

        while (!TransitionIn.transitionDone) yield return null;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
