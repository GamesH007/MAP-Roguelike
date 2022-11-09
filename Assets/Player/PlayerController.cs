using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    private float distance = 5f;

    public float maxHealth = 10;
    private float currentHp;

    public float fireRate = 1f;
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
    private float nextShotTime = 0.15f;

    public float roomDistance = 2.3f;
    public float cameraDistanceY = 11;
    public float cameraDistanceX = 25;

    // Start is called before the first frame update
    void Start()
    {

        DownShot = projectileDOWN.GetComponent<ProjectileDown>();
        UpShot = projectileUP.GetComponent<ProjectileUp>();
        LeftShot = projectileLEFT.GetComponent<ProjectileLeft>();
        RightShot = projectileRIGHT.GetComponent<ProjectileRight>();

        currentHp = maxHealth;
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

        Fire();

        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + distance * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - distance * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x - distance * speed * Time.deltaTime, transform.position.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + distance * speed * Time.deltaTime, transform.position.y);
        }

        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Time.time > nextShotTime)
        {
            _rotation = Vector2.up;
            Instantiate(projectileUP, transform.position, Quaternion.Euler(_rotation));
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.DownArrow) && Time.time > nextShotTime)
        {
            _rotation = Vector2.down;
            Instantiate(projectileDOWN, transform.position, Quaternion.Euler(_rotation));
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Time.time > nextShotTime)
        {
            _rotation = Vector2.left;
            Instantiate(projectileLEFT, transform.position, Quaternion.Euler(_rotation));
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.RightArrow) && Time.time > nextShotTime)
        {
            _rotation = Vector2.right;
            Instantiate(projectileRIGHT, transform.position, Quaternion.Euler(_rotation));
            nextShotTime = Time.time + (cooldown / fireRate);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DoorUp")
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + roomDistance);
            Camera.main.transform.position = new Vector2(transform.position.x, transform.position.y + cameraDistanceY);
        }
        if (collision.gameObject.tag == "DoorDown")
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - roomDistance);
            Camera.main.transform.position = new Vector2(transform.position.x, transform.position.y - cameraDistanceY);
        }
        if (collision.gameObject.tag == "DoorRight")
        {
            transform.position = new Vector2(transform.position.x + roomDistance, transform.position.y);
            Camera.main.transform.position = new Vector2(transform.position.x + cameraDistanceX, transform.position.y);
        }
        if (collision.gameObject.tag == "DoorLeft")
        {
            transform.position = new Vector2(transform.position.x - roomDistance, transform.position.y);
            Camera.main.transform.position = new Vector2(transform.position.x - cameraDistanceX, transform.position.y);
        }
    }
}
