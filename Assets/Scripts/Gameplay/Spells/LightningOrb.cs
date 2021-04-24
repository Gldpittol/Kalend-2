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

        damagePerTick = GetComponent<Damager>().damage;

        foreach (Collider2D col in enemies)
        {
            //damage enemy
        }

        i += Time.deltaTime;

        if (i < duration) StartCoroutine(DamageOverTime());
        else Destroy(this.gameObject);
    }
}
