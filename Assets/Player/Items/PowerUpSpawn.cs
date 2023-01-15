using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject Strenght;
    public GameObject Speed;
    public GameObject Health;
    public GameObject ShotSpeed;
    public GameObject FireRate;

    int rand;

    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, 5);

        if (rand == 0)
        {
            Instantiate(Strenght, transform.position, transform.rotation);
        }
        if (rand == 1)
        {
            Instantiate(Speed, transform.position, transform.rotation);
        }
        if (rand == 2)
        {
            Instantiate(Health, transform.position, transform.rotation);
        }
        if (rand == 3)
        {
            Instantiate(ShotSpeed, transform.position, transform.rotation);
        }
        if (rand == 4)
        {
            Instantiate(FireRate, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
