using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Utilities : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject loadButton;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CanLoad")) loadButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Corridor", LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        optionsCanvas.SetActive(!optionsCanvas.activeInHierarchy);
    }
}
