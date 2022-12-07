using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : MonoBehaviour
{
    float fireRateUp = 1;
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
            stats.fireRate = stats.fireRate + fireRateUp;
        }

        Destroy(gameObject);
    }
}
