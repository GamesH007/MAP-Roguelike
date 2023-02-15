using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileUp : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 1;
    private float distance = 131250f;

    public float damage = 1;

    public Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(rot * distance * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GenericEnemy target))
        {
            target.TakeDamage(damage);
        }
        if (collision.gameObject.TryGetComponent(out TurretBossScript turtar))
        {
            turtar.TakeDamage(damage);
        }
        if (collision.gameObject.TryGetComponent(out DukeOfFliesScript protar))
        {
            protar.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
