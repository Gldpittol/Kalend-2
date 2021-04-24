using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivineThunder : MonoBehaviour
{
    public float fadeDuration;
    public float i = 1;
    public SpriteRenderer sr;

    private void Update()
    {
        i -= Time.deltaTime / fadeDuration;

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a);
        if (i < 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().TakeDamage(GetComponent<Damager>().damage);
        }
    }
}
