using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    private float distance = 0.01f;

    public float health = 10;

    public float fireRate = 1;
    public float damage = 1;

    public GameObject projectileUP;
    public GameObject projectileDOWN;
    public GameObject projectileLEFT;
    public GameObject projectileRIGHT;
    ProjectileDown  DownShot;
    ProjectileUp UpShot;
    ProjectileLeft LeftShot;
    ProjectileRight RightShot;

    public float ShotSpeed = 1;

    private Vector2 _rotation;
    private float cooldown = 2f;
    private float lastShotTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        DownShot = projectileDOWN.GetComponent<ProjectileDown>();
        UpShot = projectileUP.GetComponent<ProjectileUp>();
        LeftShot = projectileLEFT.GetComponent<ProjectileLeft>();
        RightShot = projectileRIGHT.GetComponent<ProjectileRight>();
    }

    // Update is called once per frame
    void Update()
    {
        DownShot.speed = ShotSpeed;
        LeftShot.speed = ShotSpeed;
        RightShot.speed = ShotSpeed;
        UpShot.speed = ShotSpeed;
        DownShot.damage = damage;
        LeftShot.damage = damage;
        RightShot.damage = damage;
        UpShot.damage = damage;

        if (Time.time > lastShotTime + cooldown / fireRate)
        {
            lastShotTime = Time.time;
            Fire();
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + distance * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - distance * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x - distance * speed, transform.position.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + distance * speed, transform.position.y);
        }
    }
    private void Fire()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rotation = Vector2.up;
            Instantiate(projectileUP, transform.position, Quaternion.Euler(_rotation));
            new WaitForSeconds(2);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rotation = Vector2.down;
            Instantiate(projectileDOWN, transform.position, Quaternion.Euler(_rotation));
            new WaitForSeconds(2);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rotation = Vector2.left;
            Instantiate(projectileLEFT, transform.position, Quaternion.Euler(_rotation));
            new WaitForSeconds(2);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rotation = Vector2.right;
            Instantiate(projectileRIGHT, transform.position, Quaternion.Euler(_rotation));
            new WaitForSeconds(2);
        }

    }
}
