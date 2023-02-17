using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject[] hearts = new GameObject[20];
    public GameObject[] trest = new GameObject[169];
    public int[] position = new int[2]; // at start 6 6 
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
    private float cooldown = 1.5f;
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

    Animator a;

    Vector3 shotD;

    bool dead = true;

    bool fullscreenOn = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(960, 540, false);

        UpShot = projectileUP.GetComponent<ProjectileUp>();

        for (int i = 0; i < Stats.Length; i++)
        {
            texts[i] = Stats[i].GetComponent<TextMeshProUGUI>();
        }

        currentHp = maxHealth;

        Cursor.visible = false;
         a = GetComponent<Animator>();
        shotD = transform.right;
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
            rb.AddForce(transform.up.normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
            a.SetBool("Walking", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.up.normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
            a.SetBool("Walking", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.rotation.y == 0)
            {
                transform.Rotate(0,180,0);
                shotD = -transform.right;
            }
            rb.AddForce(transform.right.normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
            a.SetBool("Walking", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.rotation.y != 0)
            {
                transform.Rotate(0,-180,0);
                shotD = transform.right;
            }
            rb.AddForce(transform.right.normalized * distance * speed * Time.deltaTime, ForceMode2D.Impulse);
            a.SetBool("Walking", true);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S))
        {
            a.SetBool("Walking", false);
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

        if (currentHp <= 0 && dead)
        {
            a.SetBool("Dead", true);
            Time.timeScale = 0;
            Invoke("BoolSetter", 0.1f);
            Invoke("Dead" , 1);
            dead = false;
        }

        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.F11)) && !fullscreenOn)
        {
            Screen.SetResolution(1920, 1080, true);
            fullscreenOn = true;
        }
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.F11)) && fullscreenOn)
        {
            Screen.SetResolution(960, 540, false);
            fullscreenOn = false;
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
            UpShot.rot = -shotD;
            Instantiate(projectileUP, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.RightArrow) && Time.time > nextShotTime)
        {
            UpShot.rot = shotD;
            Instantiate(projectileUP, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject[,] ObjektMapa = new GameObject[13, 13];
        int l = 0;
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                ObjektMapa[i, j] = trest[l];
                l++;
            }
        }
        if (collision.gameObject.tag == "DoorUp")
        {
            ObjektMapa[position[0], position[1]].GetComponent<Image>().color = Color.cyan;
            ObjektMapa[--position[0],position[1]].GetComponent<Image>().color = Color.green;
            transform.position = new Vector2(transform.position.x, transform.position.y + roomDistanceUp);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + cameraDistanceY, Camera.main.transform.position.z);
        }
        if (collision.gameObject.tag == "DoorDown")
        {
            ObjektMapa[position[0], position[1]].GetComponent<Image>().color = Color.cyan;
            ObjektMapa[++position[0], position[1]].GetComponent<Image>().color = Color.green;
            transform.position = new Vector2(transform.position.x, transform.position.y - roomDistanceUp);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - cameraDistanceY, Camera.main.transform.position.z);
        }
        if (collision.gameObject.tag == "DoorRight")
        {
            ObjektMapa[position[0], position[1]].GetComponent<Image>().color = Color.cyan;
            ObjektMapa[position[0], ++position[1]].GetComponent<Image>().color = Color.green;
            transform.position = new Vector2(transform.position.x + roomDistanceRight, transform.position.y);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + cameraDistanceX, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (collision.gameObject.tag == "DoorLeft")
        {
            ObjektMapa[position[0], position[1]].GetComponent<Image>().color = Color.cyan;
            ObjektMapa[position[0], --position[1]].GetComponent<Image>().color = Color.green;
            transform.position = new Vector2(transform.position.x - roomDistanceRight, transform.position.y);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - cameraDistanceX, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
    }

    private void BoolSetter()
    {
        a.SetBool("Dead", false);
    }

    private void Dead()
    {
        Death.SetActive(true);
        Cursor.visible = true;
    }
}
