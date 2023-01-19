using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TurretBossProjectileScript : MonoBehaviour
{
    public float speed = 1;
    private float distance = 75000f;

    public int damage = 1;

    Rigidbody2D Projectile_Rigidbody;
    public GameObject parent;

    public float direction;

    // Start is called before the first frame update
    void Start()
    {
        Projectile_Rigidbody = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
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
