using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Flying : MonoBehaviour
{
    public float range = 2;
    public GameObject target;
    public bool detected = false;

    private Vector2 targetPos;

    public float projectileDamage = 1;

    public Vector2 Direction;

    private float cooldown = 2f;
    private float nextShotTime = 0.15f;

    public GameObject FlyingProjectile;
    FlyingProjectileScript flyingProjectile;

    // Start is called before the first frame update
    void Start()
    {
        flyingProjectile = FlyingProjectile.GetComponent<FlyingProjectileScript>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //flyingProjectile.direction = Direction;

        targetPos = target.transform.position;

        Direction = targetPos - (Vector2)transform.position;

        flyingProjectile.direction = Direction;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, range);

        if (rayInfo == true && rayInfo.collider.gameObject.tag == "Player")
        {
            detected = true;
        }
        if (rayInfo == false || rayInfo.collider.gameObject.tag != "Player")
        {
            detected = false;
        }

        if (detected)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(FlyingProjectile, transform.position, Quaternion.LookRotation(Direction));
                nextShotTime = Time.time + cooldown;
            }
        }
    }
}
