using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidBody;
    private void Update()
    {
        if(GameController.gameState == GameState.Gameplay) DecideSide();
    }

    private void FixedUpdate()
    {
        if (GameController.gameState == GameState.Gameplay) MoveCharacter();
        else
        {
            rigidBody.velocity = new Vector2(0, 0);
            GetComponent<PlayerAnimations>().animator.Play("PlayerIdle");
        }

    }


    private void DecideSide()
    {
        float mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float myPosX = transform.position.x;
        
        if ((mousePosX > myPosX) && transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if ((mousePosX < myPosX) && transform.localScale.x >0)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
    private void MoveCharacter()
    {
        rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
    }
}
