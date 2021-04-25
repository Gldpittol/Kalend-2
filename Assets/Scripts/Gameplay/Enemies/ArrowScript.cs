using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float damage;
    public AudioSource audSource;
    public AudioClip startClip;
    public AudioClip destroyClip;


    private void Awake()
    {
        audSource.PlayOneShot(startClip);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CharacterCollision.instance.PlayerTakeDamage(damage);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<ArrowScript>().enabled = false;
            audSource.PlayOneShot(destroyClip);

            Destroy(this.gameObject,3);
        }

        if(collision.CompareTag("Border"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<ArrowScript>().enabled = false;
            audSource.PlayOneShot(destroyClip);

            Destroy(this.gameObject, 3);
        }
    }
}
