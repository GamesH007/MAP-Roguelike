using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
    public float speed = 1;
    private float distance = 0.0025f;

    private GameObject player;

    public float maxHealth = 10;
    private float currentHp;

    private float collisionDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentHp = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.position = new Vector2(transform.position.x + distance * speed, transform.position.y);
        }
        if (player.transform.position.x < transform.position.x)
        {
            transform.position = new Vector2(transform.position.x - distance * speed, transform.position.y);
        }
        if (player.transform.position.y > transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + distance * speed);
        }
        if (player.transform.position.y < transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - distance * speed);
        }

        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController target))
        {
            target.TakeDamage(collisionDamage);
        }
    }
}
