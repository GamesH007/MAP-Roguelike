using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public float fireRate = 1;

    public GameObject projectile;

    private Vector2 _rotation;
    private float cooldown = 2f;
    private float lastShotTime = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastShotTime+cooldown/fireRate)
        {
            lastShotTime = Time.time;
            Fire();
        }
    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rotation = Vector2.up; 
            Instantiate(projectile, transform.position, Quaternion.Euler(_rotation));
            new WaitForSeconds(2);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rotation = Vector2.down;
            Instantiate(projectile, transform.position, Quaternion.Euler(_rotation));
            new WaitForSeconds(2);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rotation = Vector2.left;
            Instantiate(projectile, transform.position, Quaternion.Euler(_rotation));
            new WaitForSeconds(2);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rotation = Vector2.right;
            Instantiate(projectile, transform.position, Quaternion.Euler(_rotation));
            new WaitForSeconds(2);
        }

    }
}
