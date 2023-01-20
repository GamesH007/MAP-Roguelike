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

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        int rnRotStart = Random.Range(0, 360);
        rotation = new Vector3(rnRotStart, rnRotStart, rnRotStart);
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

        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
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
