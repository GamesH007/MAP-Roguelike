using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WalkShootScript : MonoBehaviour
{
    GameObject player;

    private Vector2 rotation;
    private float cooldown = 2f;
    private float nextShotTime = 0.15f;

    private float playerPosX;
    private float playerPosY;
    private float thisPosX;
    private float thisPosY;

    public GameObject EnProjDown;
    public GameObject EnProjUp;
    public GameObject EnProjRight;
    public GameObject EnProjLeft;

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
                Instantiate(EnProjUp, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
        if (playerPosX == thisPosX && playerPosY < thisPosY)
        {
            if (Time.time > nextShotTime && rayInfo.collider.gameObject.tag == "Player")
            {
                Instantiate(EnProjDown, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
        if (playerPosX > thisPosX && playerPosY == thisPosY)
        {
            if (Time.time > nextShotTime && rayInfo.collider.gameObject.tag == "Player")
            {
                Instantiate(EnProjRight, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
        if (playerPosX < thisPosX && playerPosY == thisPosY)
        {
            if (Time.time > nextShotTime && rayInfo.collider.gameObject.tag == "Player")
            {
                Instantiate(EnProjLeft, transform.position, Quaternion.Euler(rotation));
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
