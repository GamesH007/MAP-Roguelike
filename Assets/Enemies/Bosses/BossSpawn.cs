using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    float dis = 18;
    public GameObject TurBoss;
    public GameObject FlyBoss;

    private GameObject Player;
    Vector2 direction;

    int rand;

    bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        rand = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        direction = Player.transform.position - transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, dis);

        if (rayInfo == true && rayInfo.collider.gameObject.tag == "Player")
        {
            if (!spawned)
            {
                if (rand == 0)
                {
                    Instantiate(FlyBoss, transform.position, new Quaternion(0,0,0,0));
                    spawned = true;
                }
                if (rand == 1)
                {
                    Instantiate(TurBoss, transform.position, new Quaternion(0, 0, 0, 0));
                    spawned = true;
                }
            }
        }
    }
}
