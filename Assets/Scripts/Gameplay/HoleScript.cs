using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleScript : MonoBehaviour
{
    public BoxCollider2D holeCollider;

    public void HoleBehaviour()
    {
        SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
    }
}
