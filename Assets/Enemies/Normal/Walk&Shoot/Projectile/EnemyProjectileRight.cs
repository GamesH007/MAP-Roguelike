using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileRight : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 1;
    private float distance = 75000f;

    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.right * distance * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController target))
        {
            target.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
