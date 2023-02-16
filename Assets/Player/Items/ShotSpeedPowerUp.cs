using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSpeedPowerUp : MonoBehaviour
{
    float shotSpeedUp = 0.5f;
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
            stats.shotSpeed = stats.shotSpeed + shotSpeedUp;
        }

        Destroy(gameObject);
    }
}
