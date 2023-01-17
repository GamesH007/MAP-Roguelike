using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public float range = 5;
    private GameObject target;
    public bool detected = false;

    private Vector2 targetPos;

    public int projectileDamage = 1;

    public Vector2 Direction;

    private float cooldown = 2f;
    private float nextShotTime = 0.15f;

    public GameObject turret;

    public GameObject TurretProjectile;
    TurretProjectileScript turretProjectile;

    // Start is called before the first frame update
    void Start()
    {
        turretProjectile = TurretProjectile.GetComponent<TurretProjectileScript>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        turretProjectile.damage = projectileDamage;

        targetPos = target.transform.position;

        Direction = targetPos - (Vector2)transform.position;

        turretProjectile.direction = Direction;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, range);

        if (rayInfo == true && rayInfo.collider.gameObject.tag == "Player")
        {
            detected = true;
        }
        if (rayInfo == false && rayInfo.collider.gameObject.tag != "Player")
        {
            detected = false;
        }

        if (detected)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(TurretProjectile, transform.position, Quaternion.Euler(Direction));
                nextShotTime = Time.time + cooldown;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
