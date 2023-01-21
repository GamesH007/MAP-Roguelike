using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject[] hearts = new GameObject[20];
    public Rigidbody2D rb;

    public float speed = 1;
    private float distance = 3000f;
    public int maxHealth = 10;
    public int currentHp;
    public float fireRate = 1f;
    public float damage = 1;

    public GameObject projectileUP;
    ProjectileUp UpShot;

    public float shotSpeed = 1.5f;

    private Vector2 _rotation;
    private float cooldown = 2f;
    private float nextShotTime = 0.15f;

    public float roomDistanceUp = 12f;
    public float roomDistanceRight = 12f;
    public float cameraDistanceY = 17f;
    public float cameraDistanceX = 24.5f;

    public GameObject Death;
    public GameObject PauseMenu;
    bool gamePaused = false;

    float damageTaken;
    float damageCool = 0.5f;

    public GameObject[] Stats = new GameObject[4];
    TextMeshProUGUI[] texts = new TextMeshProUGUI[4];

    // Start is called before the first frame update
    void Start()
    {
        UpShot = projectileUP.GetComponent<ProjectileUp>();

        for (int i = 0; i < Stats.Length; i++)
        {
            texts[i] = Stats[i].GetComponent<TextMeshProUGUI>();
        }

        currentHp = maxHealth;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {        
        UpShot.speed = shotSpeed;
        UpShot.damage = damage;

        texts[0].text = "Damage - " + damage;
        texts[1].text = "Speed - " + speed;
        texts[2].text = "Fire rate - " + fireRate;
        texts[3].text = "Shot speed - " + shotSpeed;

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

        for (int i = currentHp; i > 0; i--)
        {
            hearts[i].SetActive(true);
        }

        if (currentHp <= 0)
        {
            Destroy(gameObject);
            Death.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    public void TakeDamage(int dmg)
    {
        if (Time.time >= damageTaken + damageCool)
        {
            hearts[currentHp].SetActive(false);
            currentHp -= dmg;
            damageTaken = Time.time;
        }
    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Time.time > nextShotTime)
        {
            UpShot.rot = transform.up;
            Instantiate(projectileUP, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.DownArrow) && Time.time > nextShotTime)
        {
            UpShot.rot = -transform.up;
            Instantiate(projectileUP, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Time.time > nextShotTime)
        {
            UpShot.rot = -transform.right;
            Instantiate(projectileUP, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.RightArrow) && Time.time > nextShotTime)
        {
            UpShot.rot = transform.right;
            Instantiate(projectileUP, transform.position, transform.rotation);
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
