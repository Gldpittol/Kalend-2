using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2OrbScript : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Fountain"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            CharacterCollision.instance.PlayerTakeDamage(damage);

            Destroy(this.gameObject);
        }
    }
}
