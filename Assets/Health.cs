using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 10;
    private float currentHp;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
    }
}
