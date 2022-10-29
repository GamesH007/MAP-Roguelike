using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public float range = 5;
    private GameObject target;
    public bool detected = false;

    private Vector2 Direction;

    public GameObject turret;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = target.transform.position;

        Direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, range);

        if (rayInfo == true && rayInfo.collider.gameObject.tag == "Player")
        {
            detected = true;
            turret.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (rayInfo == false || rayInfo.collider.gameObject.tag != "Player")
        {
            detected = false;
            turret.GetComponent<SpriteRenderer>().color = Color.green;
        }

        if (detected)
        {
            transform.up = Direction;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
