using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Orb : MonoBehaviour
{
    public float currentDuration;

    public EnemyController enemyController;

    public bool isDamagingPlayer;

    private void Update()
    {
        currentDuration += Time.deltaTime;

        if (currentDuration > enemyController.projectileDuration) Destroy(this.gameObject);

        if(isDamagingPlayer) CharacterCollision.instance.PlayerTakeDamage(enemyController.baseDamage * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDamagingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDamagingPlayer = false;
        }
    }


}
