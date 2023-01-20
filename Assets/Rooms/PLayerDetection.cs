using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PLayerDetection : MonoBehaviour
{
    float dis = 18;
    public GameObject[] Enemies = new GameObject[2];
    public GameObject[] SpawnPoints = new GameObject[2];

    private GameObject Player;
    Vector2 direction;

    bool spawned = false;
    int enemiesSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
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
                for (int i = 0; i < SpawnPoints.Length; i++)
                {
                    Instantiate(Enemies[Random.Range(0, Enemies.Length)], SpawnPoints[i].transform.position, new Quaternion(0, 0, 0, 0));
                    enemiesSpawned++;
                }
                spawned = true;
            }
        }
    }
}
