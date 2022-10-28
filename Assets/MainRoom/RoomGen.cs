using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public GameObject norRoom;
    public GameObject endRoom;
    public GameObject iteRoom;
    public Transform parent;
    public Vector2 newPosition;
    public Quaternion newRotation;
    int moveRight = 25;
    int moveLeft = -25;
    int moveUp = 11;
    int moveDown = -11;
    static int x = 11; // use odd numbers (size of map)
    static int y = 11;
    int[,] map = new int[x, y];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <x; i++)
        {
            for (int z = 0; z < y; z++)
            {
                map[i, z] = 0;
            }
        }
        map = GetVeinDown(map);
        map = GetVeinUp(map);
        map = GetVeinRight(map);
        map = GetVeinLeft(map);
        int rn = Random.Range(0, 4);
        if (rn == 0 )
        {
            for (int i = 0; i < x; i++)
            {
                if (map[i, 0] == 1)
                {
                    map[i, 0] = 3;
                }
                
            }
            for (int i = 0; i < x; i++)
            {
                if (map[i, y - 1] == 1)
                {
                    map[i, y - 1] = 2;
                }
            }
        }
        if (rn == 1)
        {
            for (int i = 0; i < y; i++)
            {
                if (map[0, i] == 1)
                {
                    map[0, i] = 3;
                }

            }
            for (int i = 0; i < y; i++)
            {
                if (map[x - 1, i] == 1)
                {
                    map[x - 1, i] = 2;
                }
            }
        }
        if (rn == 2)
        {
            for (int i = 0; i < x; i++)
            {
                if (map[i, y - 1] == 1)
                {
                    map[i, y - 1] = 3;
                }

            }
            for (int i = 0; i < x; i++)
            {
                if (map[i , 0] == 1)
                {
                    map[i , 0] = 2;
                }
            }
        }
        if (rn == 3)
        {
            for (int i = 0; i < y; i++)
            {
                if (map[x - 1, i] == 1)
                {
                    map[x - 1, i] = 3;
                }

            }
            for (int i = 0; i < y; i++)
            {
                if (map[0, i] == 1)
                {
                    map[0, i] = 2;
                }
            }
        }



        int changeLeft;
        int changeup;
        for (int i = 0; i < x; i++)
        {
            for (int z = 0; z < y; z++)
            {
                
                newPosition[0] = moveRight * i;
                newPosition[1] = moveDown * z;
                changeLeft = x / 2;
                newPosition[0] += moveLeft * changeLeft;
                changeup = y / 2;
                newPosition[1] += moveUp * changeup;
                if (map[i, z] == 1)
                {
                    if (i != changeLeft || z != changeup) { GameObject a = Instantiate(norRoom, newPosition, newRotation, parent); };
                }
                if (map[i, z] == 2)
                {
                    if (i != changeLeft || z != changeup) { GameObject a = Instantiate(iteRoom, newPosition, newRotation, parent); };
                }
                if (map[i, z] == 3)
                {
                    if (i != changeLeft || z != changeup) { GameObject a = Instantiate(endRoom, newPosition, newRotation, parent); };
                }
            }
        }

    }
    static int[,] GetVeinDown(int[,] r )
    {
        int down = x / 2;
        int vein = y / 2;
        int[,] map = r;
        int rn;
        int f = y / 2;
        for (int i = 0; i < f; i++)
        {
            rn = Random.Range(0, 100);
            if (rn <= 20)
            {
                vein++;
                map[vein, down] = 1;
                map[vein, ++down] = 1;
            }
            if (rn > 20 && rn <= 40)
            {
                vein--;
                map[vein, down] = 1;
                map[vein, ++down] = 1;
            }
            if (rn > 40 && rn < 100)
            {
                map[vein, ++down] = 1;
            }
        }
        return map;
    }
    static int[,] GetVeinUp(int[,] r)
    {
        int up = x / 2;
        int vein = y / 2;
        int[,] map = r;
        int rn;
        int f = y / 2;
        for (int i = 0; i < f; i++)
        {
            rn = Random.Range(0, 100);
            if (rn <= 20)
            {
                vein++;
                map[vein, up] = 1;
                map[vein, --up] = 1;
            }
            if (rn > 20 && rn <= 40)
            {
                vein--;
                map[vein, up] = 1;
                map[vein, --up] = 1;
            }
            if (rn > 40 && rn < 100)
            {
                map[vein, --up] = 1;
            }
        }
        return map;
    }
    static int[,] GetVeinRight(int[,] r)
    {
        int right = x / 2;
        int vein = y / 2;
        int[,] map = r;
        int rn;
        int f = y / 2;
        for (int i = 0; i < f; i++)
        {
            rn = Random.Range(0, 100);
            if (rn <= 20)
            {
                vein++;
                map[right, vein] = 1;
                map[++right, vein] = 1;
            }
            if (rn > 20 && rn <= 40)
            {
                vein--;
                map[right, vein] = 1;
                map[++right, vein] = 1;
            }
            if (rn > 40 && rn < 100)
            {
                map[++right, vein] = 1;
            }
        }
        return map;
    }
    static int[,] GetVeinLeft(int[,] r)
    {
        int left = x / 2;
        int vein = y / 2;
        int[,] map = r;
        int rn;
        int f = y / 2;
        for (int i = 0; i < f; i++)
        {
            rn = Random.Range(0, 100);
            if (rn <= 20)
            {
                vein++;
                map[left, vein] = 1;
                map[--left, vein] = 1;
            }
            if (rn > 20 && rn <= 40)
            {
                vein--;
                map[left, vein] = 1;
                map[--left, vein] = 1;
            }
            if (rn > 40 && rn < 100)
            {
                map[--left, vein] = 1;
            }
        }
        return map;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    
 
}
