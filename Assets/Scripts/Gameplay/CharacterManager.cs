using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject exclamationMark;
    public List<GameObject> interactablesCollisionList;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Fountain"))
        {
            exclamationMark.SetActive(true);
            interactablesCollisionList.Add(collision.gameObject);
        }

        if(collision.CompareTag("Hole"))
        {
            exclamationMark.SetActive(true);
            interactablesCollisionList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fountain"))
        {
            interactablesCollisionList.Remove(collision.gameObject);
            if(interactablesCollisionList.Count == 0) exclamationMark.SetActive(false);
        }
        if (collision.CompareTag("Hole"))
        {
            interactablesCollisionList.Remove(collision.gameObject);
            if (interactablesCollisionList.Count == 0) exclamationMark.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && GameController.gameState == GameState.Gameplay)
        {
            HandleInteractionInput();
        }
    }

    private void HandleInteractionInput()
    {
        if(interactablesCollisionList.Count > 0)
        {
            HoleScript hole = interactablesCollisionList[0].GetComponent<HoleScript>();
            if (hole)
            {
                hole.HoleBehaviour();
            }

            FountainScript fountain = interactablesCollisionList[0].GetComponent<FountainScript>();
            if(fountain)
            {
                fountain.OpenFountain();
            }
        }
    }
}
