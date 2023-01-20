using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 1;
    private float distance = 1000f;

    private GameObject player;

    public float maxHealth = 10;
    private float currentHp;

    private int collisionDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentHp = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.x > transform.position.x)
        {
            rb.AddForce((transform.right).normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (player.transform.position.x < transform.position.x)
        {
            rb.AddForce((-transform.right).normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (player.transform.position.y > transform.position.y)
        {
            rb.AddForce((transform.up).normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (player.transform.position.y < transform.position.y)
        {
            rb.AddForce((-transform.up).normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
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
