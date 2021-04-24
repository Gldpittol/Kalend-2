using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isSpecialAttack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collider2D collision)
    {
        if(collision.CompareTag("Border"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            if (!isSpecialAttack) Destroy(this.gameObject);
            else GetComponent<Damager>().damage *= 0.9f;
        }
    }
}
