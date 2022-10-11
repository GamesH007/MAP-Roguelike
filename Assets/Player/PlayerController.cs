using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    private float distance = 0.01f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + distance * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - distance * speed);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x - distance * speed, transform.position.y);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + distance * speed, transform.position.y);
        }       
    }
}
