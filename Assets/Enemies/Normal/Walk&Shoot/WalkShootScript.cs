using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkShootScript : MonoBehaviour
{
    GameObject player;

    private Vector2 rotation;
    private float cooldown = 2f;
    private float nextShotTime = 0.15f;

    public float projectDamage = 1;

    public GameObject EnProjDown;
    public GameObject EnProjUp;
    public GameObject EnProjRight;
    public GameObject EnProjLeft;

    EnemyProjectileDown eneProjecDown;
    EnemyProjectileUp eneProjecUp;
    EnemyProjectileRight eneProjecRight;
    EnemyProjectileLeft eneProjecLeft;

    // Start is called before the first frame update
    void Start()
    {
        eneProjecDown = EnProjDown.GetComponent<EnemyProjectileDown>();
        eneProjecRight = EnProjRight.GetComponent<EnemyProjectileRight>();
        eneProjecUp = EnProjUp.GetComponent<EnemyProjectileUp>();
        eneProjecLeft = EnProjLeft.GetComponent<EnemyProjectileLeft>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        eneProjecDown.damage = projectDamage;
        eneProjecLeft.damage = projectDamage;
        eneProjecRight.damage = projectDamage;
        eneProjecUp.damage = projectDamage;

        if (player.transform.position.x == transform.position.x && player.transform.position.y > transform.position.y)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(EnProjUp, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
        if (player.transform.position.x == transform.position.x && player.transform.position.y < transform.position.y)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(EnProjDown, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
        if (player.transform.position.y == transform.position.y && player.transform.position.x > transform.position.x)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(EnProjRight, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
        if (player.transform.position.y == transform.position.y && player.transform.position.x < transform.position.x)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(EnProjLeft, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
    }
}
