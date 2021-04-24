using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningOrb : MonoBehaviour
{
    public float damagePerTick;
    public List<Collider2D> enemies = new List<Collider2D>();
    float i = 0;
    public float duration;
    public float delayBetweenHits;

    private void Start()
    {
        StartCoroutine(DamageOverTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Remove(collision);
        }
    }
    public IEnumerator DamageOverTime()
    {
        yield return delayBetweenHits;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].CompareTag("Enemy"))
            {
                enemies[i].GetComponent<EnemyController>().TakeDamage(GetComponent<Damager>().damage * Time.deltaTime);
            }
        }

        i += Time.deltaTime;

        if (i < duration) StartCoroutine(DamageOverTime());
        else Destroy(this.gameObject);
    }
}
