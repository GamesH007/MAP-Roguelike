using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    ProjectileUp Shot;

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

    public GameObject[] stats = new GameObject[4];
    TextMeshProUGUI[] texts = new TextMeshProUGUI[4];

    Animator a;

    Vector3 shotD;

    bool dead = true;

    public int disabled = 0;

    // Start is called before the first frame update
    void Start()
    {
        Shot = projectileUP.GetComponent<ProjectileUp>();

        for (int i = 0; i < stats.Length; i++)
        {
            texts[i] = stats[i].GetComponent<TextMeshProUGUI>();
        }
        
        a = GetComponent<Animator>();

        Cursor.visible = false;
        shotD = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        Shot.speed = shotSpeed;
        Shot.damage = damage;

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
            Invoke("BoolSetter", 0.1f);
            Invoke("Dead" , 1);
            dead = false;
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
            Shot.rot = transform.up;
            Instantiate(projectileUP, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.DownArrow) && Time.time > nextShotTime)
        {
            Shot.rot = -transform.up;
            Instantiate(projectileUP, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Time.time > nextShotTime)
        {
            Shot.rot = -shotD;
            Instantiate(projectileUP, transform.position, transform.rotation);
            nextShotTime = Time.time + (cooldown / fireRate);
        }
        if (Input.GetKey(KeyCode.RightArrow) && Time.time > nextShotTime)
        {
            Shot.rot = shotD;
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
        if (collision.gameObject.tag == "LowerLevel")
        {
            SceneManager.LoadScene("SampleScene");
            disabled++;
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("dmg", damage);
        PlayerPrefs.SetFloat("shSpeed", shotSpeed);
        PlayerPrefs.SetFloat("fiRate", fireRate);
        if (currentHp != 0)
        {
            PlayerPrefs.SetInt("health", currentHp);
        }
        else
        {
            PlayerPrefs.SetInt("health", maxHealth);
        }
        PlayerPrefs.SetFloat("movementSpeed", speed);
        PlayerPrefs.SetInt("dis", disabled);
    }

    private void OnEnable()
    {
        disabled = PlayerPrefs.GetInt("dis");

        if (disabled > 0)
        {
            damage = PlayerPrefs.GetFloat("dmg");
            shotSpeed = PlayerPrefs.GetFloat("shSpeed");
            fireRate = PlayerPrefs.GetFloat("fiRate");
            maxHealth = PlayerPrefs.GetInt("health");
            speed = PlayerPrefs.GetFloat("movementSpeed");
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
