using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        EnemyFire();
    }

    private void EnemyFire()
    {
        if (playerPosX == thisPosX && playerPosY > thisPosY)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(EnProjUp, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
        if (playerPosX == thisPosX && playerPosY < thisPosY)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(EnProjDown, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
        if (playerPosX > thisPosX && playerPosY == thisPosY)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(EnProjRight, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
        if (playerPosX < thisPosX && playerPosY == thisPosY)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(EnProjLeft, transform.position, Quaternion.Euler(rotation));
                nextShotTime = Time.time + cooldown;
            }
        }
    }
}
