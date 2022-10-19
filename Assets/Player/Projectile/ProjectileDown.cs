using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDown : MonoBehaviour
{
    public float speed = 1;
    private float distance = 0.025f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - distance * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
