using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CharacterCollision.instance.PlayerTakeDamage(damage);
            Destroy(this.gameObject);
        }

        if(collision.CompareTag("Border"))
        {
            Destroy(this.gameObject);
        }
    }
}
