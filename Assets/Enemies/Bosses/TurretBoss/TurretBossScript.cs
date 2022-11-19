using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBossScript : MonoBehaviour
{
    public float range = 5;
    private GameObject target;
    public bool detected = false;

    public float projectileDamage = 1;

    private Vector2 Direction;

    public float cooldown = 2f;
    private float nextShotTime;
    private float rotation;
    int rnAttack = 0;
    int attacked = 0;

    public GameObject turret;

    public GameObject TurretProjectile;
    TurretBossProjectileScript turretProjectile;

    // Start is called before the first frame update
    void Start()
    {
        turretProjectile = TurretProjectile.GetComponent<TurretBossProjectileScript>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        turretProjectile.damage = projectileDamage;
        Vector2 targetPos = target.transform.position;

        Direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, range);

        if (rayInfo == true && rayInfo.collider.gameObject.tag == "Player")
        {
            detected = true;
            turret.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (rayInfo == false)
        {
            detected = false;
            turret.GetComponent<SpriteRenderer>().color = Color.green;
        }

        
        if (Time.time > nextShotTime)
        {
            for (attacked = 0; attacked < 1;)
            {
                rotation = transform.rotation.z;
                rnAttack = Random.Range(0, 2);
                FireType(rnAttack);
            }
        }
    }

    public int FireType(int x)
    {
        if (x == 0)
        {
            //circle
            for (int i = 0; i < 8; i++)
            {
                Instantiate(TurretProjectile, transform.position, transform.rotation);
                turretProjectile.direction = rotation;
                rotation += 45;
            }
            attacked = 1;
            nextShotTime = Time.time + cooldown;
        }
        if (x == 1)
        {
            //tenticle circle

            for (int i = 0; i < 6; i++)
            {
                for (int e = 0; e < 3; e++)
                {
                    Instantiate(TurretProjectile, transform.position + new Vector3(0.0015f, 0.0015f, 0), transform.rotation);
                    turretProjectile.direction = rotation;
                    rotation += 15;
                }
                rotation += 60;
            }
            attacked = 1;
            nextShotTime = Time.time + cooldown;
        }
        return attacked;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
