using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGen : MonoBehaviour
{
    float dis = 20;
    public GameObject door;
    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, transform.up, dis);

        if (rayInfo != true || rayInfo.collider.gameObject.tag != "Wall")
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
        //RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, dis, -transform.up);

        //if (hit2D.collider.gameObject.tag == "Enemy")
        //{
        //    door.SetActive(false);
        //}
        //else
        //{
        //    if (active)
        //    {
        //        door.SetActive(true);
        //    }
        //    else
        //    {
        //        door.SetActive(false);
        //    }
        //}

        RaycastHit2D rayRooms = Physics2D.Raycast(transform.position, transform.up, dis);

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
