using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBossScript : MonoBehaviour
{
    public float range = 5;
    private GameObject target;
    public bool detected = false;

    public int projectileDamage = 1;

    private Vector2 Direction;

    public float cooldown = 1f;
    private float nextShotTime;
    public float rotation;
    int rnAttack = 0;
    int attacked = 0;

    public GameObject turret;

    public GameObject TurretProjectile;
    TurretBossProjectileScript turretProjectile;

    public float maxHealth = 10;
    private float currentHp;

    private int collisionDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHealth;
        turretProjectile = TurretProjectile.GetComponent<TurretBossProjectileScript>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShotTime)
        {
            for (attacked = 0; attacked < 1;)
            {
                rotation = 0;
                rnAttack = UnityEngine.Random.Range(0, 2);
                FireType(rnAttack);
            }
        }

        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int FireType(int x)
    {
        if (x == 0)
        {
            rotation = 0;
            //circle
            for (int i = 0; i < 8; i++)
            {
                Instantiate(TurretProjectile, transform.position, transform.rotation);
                turretProjectile.direction = rotation;
                rotation += 45;
            }
            attacked = 1;
            rotation = 0;
            nextShotTime = Time.time + cooldown;
        }
        if (x == 1)
        {
            rotation = Random.Range(0, 90);
            //tenticle circle
            for (int i = 0; i < 3; i++)
            {
                for (int e = 0; e < 3; e++)
                {
                    Instantiate(TurretProjectile, transform.position, transform.rotation);
                    turretProjectile.direction = rotation;
                    rotation += 20;
                }
                rotation += 60;
            }
            attacked = 1;
            rotation *= 0;
            nextShotTime = Time.time + cooldown;
        }
        return attacked;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
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
