using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGen : MonoBehaviour
{
    float dis = 20;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, transform.up, dis);

        if (rayInfo != true || rayInfo.collider.gameObject.tag != "Wall")
        {
            door.SetActive(false);
        }
        else
        {
            door.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, transform.up, dis);

        //if (rayInfo != true || rayInfo.collider.gameObject.tag != "Wall")
        //{
        //    door.SetActive(false);
        //}
        //else
        //{
        //    door.SetActive(true);
        //}
    }
}
