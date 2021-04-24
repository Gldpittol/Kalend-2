using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Utilities : MonoBehaviour
{
    public GameObject optionsCanvas;
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
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
