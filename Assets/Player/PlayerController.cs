using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 1;
    private float distance = 2000f;

    public float maxHealth = 10;
    public float currentHp;

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

    public float shotSpeed = 1.5f;

    private Vector2 _rotation;
    private float cooldown = 2f;
    private float nextShotTime = 0.15f;

    public float roomDistanceUp = 3f;
    public float roomDistanceRight = 3f;
    public float cameraDistanceY = 11f;
    public float cameraDistanceX = 18.5f;

    public GameObject PauseMenu;
    bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {

        DownShot = projectileDOWN.GetComponent<ProjectileDown>();
        UpShot = projectileUP.GetComponent<ProjectileUp>();
        LeftShot = projectileLEFT.GetComponent<ProjectileLeft>();
        RightShot = projectileRIGHT.GetComponent<ProjectileRight>();

        currentHp = maxHealth;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        DownShot.speed = shotSpeed;
        LeftShot.speed = shotSpeed;
        RightShot.speed = shotSpeed;
        UpShot.speed = shotSpeed;
        DownShot.damage = damage;
        LeftShot.damage = damage;
        RightShot.damage = damage;
        UpShot.damage = damage;

        Fire();

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce((transform.up).normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce((-transform.up).normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce((-transform.right).normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce((transform.right).normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
        }

        if (gamePaused == true)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            PauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            PauseMenu.SetActive(false);
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
            Instantiate(projectileUP, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.DownArrow) && Time.time > nextShotTime)
        {
            _rotation = Vector2.down;
            Instantiate(projectileDOWN, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Time.time > nextShotTime)
        {
            _rotation = Vector2.left;
            Instantiate(projectileLEFT, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.RightArrow) && Time.time > nextShotTime)
        {
            _rotation = Vector2.right;
            Instantiate(projectileRIGHT, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DoorUp")
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + roomDistanceUp);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + cameraDistanceY, Camera.main.transform.position.z);
        }
        if (collision.gameObject.tag == "DoorDown")
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - roomDistanceUp);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - cameraDistanceY, Camera.main.transform.position.z);
        }
        if (collision.gameObject.tag == "DoorRight")
        {
            transform.position = new Vector2(transform.position.x + roomDistanceRight, transform.position.y);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + cameraDistanceX, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (collision.gameObject.tag == "DoorLeft")
        {
            transform.position = new Vector2(transform.position.x - roomDistanceRight, transform.position.y);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - cameraDistanceX, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
    }
}
