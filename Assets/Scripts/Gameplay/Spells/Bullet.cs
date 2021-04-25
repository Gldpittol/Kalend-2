using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isSpecialAttack;

    public AudioSource audSource;

    public AudioClip fireballClip;
    public AudioClip explosionClip;
    public AudioClip boulderClip;

    private void Awake()
    {
        if (isSpecialAttack)
            audSource.PlayOneShot(boulderClip);
        else
            audSource.PlayOneShot(fireballClip);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collider2D collision)
    {
        if(collision.CompareTag("Border"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Bullet>().enabled = false;

            Destroy(this.gameObject, 3);
        }

        if (collision.CompareTag("Fountain"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Bullet>().enabled = false;

            Destroy(this.gameObject, 3);
        }

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().TakeDamage(GetComponent<Damager>().damage);
            audSource.PlayOneShot(explosionClip, 0.3f);

            if (!isSpecialAttack)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Bullet>().enabled = false;

                Destroy(this.gameObject,3);
            }
            else GetComponent<Damager>().damage *= 0.9f;
        }
    }
}
