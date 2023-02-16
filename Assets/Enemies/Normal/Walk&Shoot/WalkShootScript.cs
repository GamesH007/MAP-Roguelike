using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkShootScript : MonoBehaviour
{
    GameObject player;

    private float cooldown = 2f;
    private float nextShotTime = 0.15f;
    public int Damage = 1;

    private float playerPosX;
    private float playerPosY;
    private float thisPosX;
    private float thisPosY;

    public GameObject EnProjUp;
    public GameObject EnProjDown;
    public GameObject EnProjLeft;
    public GameObject EnProjRight;

    private Vector2 Direction;

    private int collisionDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPosX = Mathf.Round(player.transform.position.x);
        playerPosY = Mathf.Round(player.transform.position.y);
        thisPosX = Mathf.Round(transform.position.x);
        thisPosY = Mathf.Round(transform.position.y);

        Vector2 targetPos = player.transform.position;

        Direction = targetPos - (Vector2)transform.position;

        EnemyFire();
    }

    private void EnemyFire()
    {
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction);

        if (playerPosX == thisPosX && playerPosY > thisPosY)
        {
            if (Time.time > nextShotTime && rayInfo.collider.gameObject.tag == "Player")
            {
                Instantiate(EnProjUp, transform.position, transform.rotation);
                nextShotTime = Time.time + cooldown;
            }
        }
        if (playerPosX == thisPosX && playerPosY < thisPosY)
        {
            if (Time.time > nextShotTime && rayInfo.collider.gameObject.tag == "Player")
            {
                Instantiate(EnProjDown, transform.position, transform.rotation);
                nextShotTime = Time.time + cooldown;
            }
        }
        if (playerPosX > thisPosX && playerPosY == thisPosY)
        {
            if (Time.time > nextShotTime && rayInfo.collider.gameObject.tag == "Player")
            {
                if (transform.rotation.y == 0)
                {
                    Instantiate(EnProjRight, transform.position, transform.rotation);
                }
                else
                {
                    Instantiate(EnProjLeft, transform.position, transform.rotation);
                }
                nextShotTime = Time.time + cooldown;
            }
        }
        if (playerPosX < thisPosX && playerPosY == thisPosY)
        {
            if (Time.time > nextShotTime && rayInfo.collider.gameObject.tag == "Player")
            {
                if (transform.rotation.y == 0)
                {
                    Instantiate(EnProjLeft, transform.position, transform.rotation);
                }
                else
                {
                    Instantiate(EnProjRight, transform.position, transform.rotation);
                }
                nextShotTime = Time.time + cooldown;
            }
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
