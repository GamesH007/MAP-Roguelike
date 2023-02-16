using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DukeOfFliesScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 1;
    private float distance = 50000f;

    public GameObject Flyer;
    private float lastSpawnTime = 5;
    public float flyerSpawnCooldown = 5;

    public float maxHealth = 10;
    private float currentHp;

    private int collisionDamage = 1;

    public Vector3 rotation;

    public Sprite phase1;
    public Sprite phase2;

    SpriteRenderer spriteRenderer;

    Animator animator;

    bool phaser1 = true;
    bool phaser2 = true;
    bool phaser3 = true;


    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        int rnRotStart = Random.Range(0, 360);
        rotation = new Vector3(rnRotStart, rnRotStart, rnRotStart);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(rotation.normalized * distance * speed * Time.deltaTime);

        if (Time.time > lastSpawnTime + flyerSpawnCooldown)
        {
            for (int i = 0; i < Random.Range(2, 3); i++)
            {
                Instantiate(Flyer, transform.position + transform.localScale / 2, transform.rotation);
            }

            lastSpawnTime = Time.time;
        }

        if (currentHp <= maxHealth/3*2 && phaser1)
        {
            animator.SetBool("Phase1", true);
            spriteRenderer.sprite = phase1;
            phaser1 = false;
        }
        if (currentHp <= maxHealth / 3 && phaser2)
        {
            animator.SetBool("Phase2", true);
            spriteRenderer.sprite = phase2;
            phaser2 = false;
        }
        if (currentHp <= 0 && phaser3)
        {
            lastSpawnTime = float.MaxValue;
            distance = 0;
            animator.SetBool("Phase3", true);
            Invoke("BoolSetter", 0.1f);
            Destroy(gameObject,1);
            phaser3 = false;
        }
    }

    private void BoolSetter()
    {
        animator.SetBool("Phase1", false);
        animator.SetBool("Phase2", false);
        animator.SetBool("Phase3", false);
    }

    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.ToLower() == "wall")
        {
            int rnRotCol = Random.Range(-45, 45);
            rotation = -collision.gameObject.transform.up.normalized + new Vector3(rnRotCol, rnRotCol, rnRotCol).normalized;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController target))
        {
            target.TakeDamage(collisionDamage);
        }
    }
}
