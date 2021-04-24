using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleScript : MonoBehaviour
{
    public BoxCollider2D holeCollider;
    public void HoleBehaviour()
    {
        CharacterManager.instance.interactablesCollisionList.Clear();

        if (PlayerPrefs.HasKey("MaxDepth"))
        {
            PlayerData.currentDepth++;
            PlayerData.maxDepth = PlayerPrefs.GetInt("MaxDepth");

            if (PlayerData.currentDepth > PlayerData.maxDepth)
            {
                PlayerData.maxDepth = PlayerData.currentDepth;
                PlayerPrefs.SetInt("MaxDepth", PlayerData.maxDepth);
                print(PlayerPrefs.GetInt("MaxDepth"));
            }

            SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
        }

        else 
        {
            PlayerData.currentDepth = 0;
            PlayerPrefs.SetInt("MaxDepth", 0);
            PlayerPrefs.SetInt("CanLoad", 1);
           
            SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        }
    }
}
