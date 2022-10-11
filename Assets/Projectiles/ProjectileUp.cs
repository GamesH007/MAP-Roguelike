using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileUp : MonoBehaviour
{
    public float speed = 1;
    private float distance = 0.025f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5);
        transform.position = new Vector2(transform.position.x, transform.position.y + distance * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
