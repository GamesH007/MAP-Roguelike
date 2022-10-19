using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRight : MonoBehaviour
{
    public float speed = 1;
    private float distance = 0.025f;

    public float damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + distance * speed, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health target))
        {
            target.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
