using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectileScript : MonoBehaviour
{
    public float speed = 1;
    private float distance = 62500f;

    public float damage = 1;

    Rigidbody2D Projectile_Rigidbody;

    public Vector2 direction;

    public GameObject Turret;
    private Quaternion projectRotation;

    // Start is called before the first frame update
    void Start()
    {
        Projectile_Rigidbody = GetComponent<Rigidbody2D>();
        projectRotation = Quaternion.Euler(transform.rotation.z * direction);

        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = projectRotation;
        Projectile_Rigidbody.AddForce(transform.up.normalized * speed * distance * Time.deltaTime);
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
