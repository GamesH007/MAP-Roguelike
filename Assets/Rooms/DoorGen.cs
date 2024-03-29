using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGen : MonoBehaviour
{
    float dis = 20;
    public GameObject door;
    public  bool active;
    public GameObject startingPoint;

    //int EnemiesInRoom = 0;
    public GameObject[] enemies = null;
    public GameObject[] bosses = null;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D rayInfo = Physics2D.Raycast(startingPoint.transform.position, transform.up, dis);

        if (rayInfo == false || rayInfo.collider.gameObject.tag != "Wall" || rayInfo.collider.gameObject.layer != 10)
        {
            door.SetActive(false);
            active = false;
        }
        else
        {
            door.SetActive(true);
            active = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bosses = GameObject.FindGameObjectsWithTag("Boss");

        if (door.transform.parent.name == "EndRoom(Clone)")
        {
            if (bosses.Length == 0 && active && enemies.Length == 0)
            {
                door.SetActive(true);
            }
            else
            {
                door.SetActive(false);
            }
        }
        else
        {
            if (enemies.Length == 0 && active)
            {
                door.SetActive(true);
            }
            else
            {
                door.SetActive(false);
            }
        }

        RaycastHit2D rayRooms = Physics2D.Raycast(startingPoint.transform.position, transform.up, dis);

        if (rayRooms == true && rayRooms.collider.gameObject.transform.parent.name == "EndRoom(Clone)")
        {
            door.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (rayRooms == true && rayRooms.collider.gameObject.transform.parent.name == "ItemRoom(Clone)")
        {
            door.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
