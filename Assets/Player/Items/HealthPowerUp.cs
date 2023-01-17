using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    int healthUp = 10;
    GameObject player;
    PlayerController stats;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerController>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController damage))
        {
            stats.maxHealth = stats.maxHealth + healthUp;
            stats.currentHp = stats.currentHp + healthUp;
        }

        Destroy(gameObject);
    }
}
