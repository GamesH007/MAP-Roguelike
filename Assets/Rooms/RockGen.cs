using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGen : MonoBehaviour
{
    public GameObject[] Rocks = null;
    public Sprite[] Sprites = null;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Rocks.Length; i++)
        {
            Rocks[i].GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
