using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStorm : MonoBehaviour
{
    public float damagePerSecond;
    public List<GameObject> enemies = new List<GameObject>();
    public int j = 0;
    public float duration;
    public float delayBetweenHits;

    public AudioSource audSource;
    public AudioClip audClip;

    private void Awake()
    {
        audSource.PlayOneShot(audClip);
    }

    private void Start()
    {
        StartCoroutine(DamageOverTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //enemies.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //enemies.Remove(collision);
        }
    }
    public IEnumerator DamageOverTime()
    {
        yield return null;

        damagePerSecond = GetComponent<Damager>().damage;

        enemies.Clear();

        if (Spawner.instance)

        {
            foreach (Transform t in Spawner.instance.enemyHolder.GetComponentInChildren<Transform>())
            {
                enemies.Add(t.gameObject);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].CompareTag("Enemy") && enemies[i].GetComponent<BoxCollider2D>())
                {
                    enemies[i].GetComponent<EnemyController>().TakeDamage(GetComponent<Damager>().damage);
                }
            }
        }

        yield return new WaitForSeconds(delayBetweenHits);

        j+=1;

        if (j < duration) StartCoroutine(DamageOverTime());
        else Destroy(this.gameObject);
    }
}
