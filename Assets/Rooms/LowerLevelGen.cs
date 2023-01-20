using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LowerLevelGen : MonoBehaviour
{
    public GameObject powerUpsSpawn;
    public GameObject[] enemies = null;
    public GameObject[] bosses = null;
    public GameObject[] powerUps = null;
    public GameObject liftDown;
    bool spawned = false;
    GameObject Player;
    Vector2 direction;
    float dis = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        liftDown.SetActive(false);
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Player.transform.position - transform.position;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bosses = GameObject.FindGameObjectsWithTag("Boss");

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, dis);

        if (rayInfo == true && rayInfo.collider.gameObject.tag == "Player")
        {
            if (bosses.Length == 0 && enemies.Length == 0 && !spawned)
            {
                liftDown.SetActive(true);
                Instantiate(powerUps[Random.Range(0, powerUps.Length)], powerUpsSpawn.transform.position, transform.rotation);
                spawned = true;
            }
        }
    }
}
