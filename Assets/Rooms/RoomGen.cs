using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomGen : MonoBehaviour
{
    public GameObject[] trest = new GameObject[169];
    public GameObject[] norRoom = new GameObject[2];
    public GameObject endRoom;
    public GameObject iteRoom;
    public GameObject MainRoom;

    float moveRight = 24.5f;
    float moveLeft = -24.5f;
    int moveUp = 17;
    int moveDown = -17;
    public int mSize = 13; // use 13 (size of map)
    
    int[,] map = new int[13, 13];

    public Sprite[] Background = null;
    int back = 0;
    bool generated = false;
         
    private void Awake()
    {
        if (!generated)
        {
            back = Random.Range(0, Background.Length);
            MainRoom.GetComponent<SpriteRenderer>().sprite = Background[back];
            generated = true;
        }

        map = new int[13, 13];
        Vector2 newPosition = new Vector2(0, 0);
        Quaternion newRotation = new Quaternion(0, 0, 0, 0);

        for (int i = 0; i < mSize; i++)
        {
            for (int z = 0; z < mSize; z++)
            {
                map[i, z] = 0;
            }
        }
        map = GetVeinDown(map, mSize);
        map = GetVeinUp(map, mSize);
        map = GetVeinRight(map, mSize);
        map = GetVeinLeft(map, mSize);
        int rn = Random.Range(0, 4);
        if (rn == 0)
        {
            for (int i = 0; i < mSize; i++)
            {
                if (map[i, 0] == 1)
                {
                    map[i, 0] = 3;
                }

            }
            for (int i = 0; i < mSize; i++)
            {
                if (map[i, mSize - 1] == 1)
                {
                    map[i, mSize - 1] = 2;
                }
            }
        }
        if (rn == 1)
        {
            for (int i = 0; i < mSize; i++)
            {
                if (map[0, i] == 1)
                {
                    map[0, i] = 3;
                }

            }
            for (int i = 0; i < mSize; i++)
            {
                if (map[mSize - 1, i] == 1)
                {
                    map[mSize - 1, i] = 2;
                }
            }
        }
        if (rn == 2)
        {
            for (int i = 0; i < mSize; i++)
            {
                if (map[i, mSize - 1] == 1)
                {
                    map[i, mSize - 1] = 3;
                }

            }
            for (int i = 0; i < mSize; i++)
            {
                if (map[i, 0] == 1)
                {
                    map[i, 0] = 2;
                }
            }
        }
        if (rn == 3)
        {
            for (int i = 0; i < mSize; i++)
            {
                if (map[mSize - 1, i] == 1)
                {
                    map[mSize - 1, i] = 3;
                }

            }
            for (int i = 0; i < mSize; i++)
            {
                if (map[0, i] == 1)
                {
                    map[0, i] = 2;
                }
            }
        }

        GameObject[,] ObjektMapa = new GameObject[13, 13];
        int l = 0;
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                ObjektMapa[i, j] = trest[l];
                l++;
            }
        }
        ObjektMapa[6, 6].SetActive(true);
        ObjektMapa[6, 6].GetComponent<Image>().color = Color.green;
        for (int i = 12; i > -1; i--)
        {
            for (int j = 12; j > -1; j--)
            {
                if (map[i,j] != 0 )
                {
                    ObjektMapa[j, i].SetActive(true);
                    
                    if (map[i,j] == 2)
                    {
                        ObjektMapa[j, i].GetComponent<Image>().color = Color.yellow;
                    }
                    if (map[i, j] == 3)
                    {
                        ObjektMapa[j, i].GetComponent<Image>().color = Color.red;
                    }
                }
            }
        }


        int changeLeft;
        int changeup;
        for (int i = 0; i < mSize; i++)
        {
            for (int z = 0; z < mSize; z++)
            {

                newPosition[0] = moveRight * i;
                newPosition[1] = moveDown * z;
                changeLeft = mSize / 2;
                newPosition[0] += moveLeft * changeLeft;
                changeup = mSize / 2;
                newPosition[1] += moveUp * changeup;
                if (map[i, z] == 1)
                {
                    if (i != changeLeft || z != changeup) { GameObject a = Instantiate(norRoom[Random.Range(0,norRoom.Length)], newPosition, newRotation);a.transform.parent = gameObject.transform; a.GetComponent<SpriteRenderer>().sprite = Background[back]; };
                }
                if (map[i, z] == 2)
                {
                    if (i != changeLeft || z != changeup) { GameObject a = Instantiate(iteRoom, newPosition, newRotation); a.transform.parent = gameObject.transform; a.GetComponent<SpriteRenderer>().sprite = Background[back]; };
                }
                if (map[i, z] == 3)
                {
                    if (i != changeLeft || z != changeup) { GameObject a = Instantiate(endRoom, newPosition, newRotation); a.transform.parent = gameObject.transform; a.GetComponent<SpriteRenderer>().sprite = Background[back]; };
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
       
        //int[,] map = new int[999,999];
        //Vector2 newPosition = new Vector2(0,0);
        //Quaternion newRotation = new Quaternion(0,0,0,0);

        //for (int i = 0; i <mSize; i++)
        //{
        //    for (int z = 0; z < mSize; z++)
        //    {
        //        map[i, z] = 0;
        //    }
        //}
        //map = GetVeinDown(map,mSize);
        //map = GetVeinUp(map,mSize);
        //map = GetVeinRight(map,mSize);
        //map = GetVeinLeft(map,mSize);
        //int rn = Random.Range(0,4);
        //if (rn == 0 )
        //{
        //    for (int i = 0; i < mSize; i++)
        //    {
        //        if (map[i, 0] == 1)
        //        {
        //            map[i, 0] = 3;
        //        }

        //    }
        //    for (int i = 0; i < mSize; i++)
        //    {
        //        if (map[i, mSize - 1] == 1)
        //        {
        //            map[i, mSize - 1] = 2;
        //        }
        //    }
        //}
        //if (rn == 1)
        //{
        //    for (int i = 0; i < mSize; i++)
        //    {
        //        if (map[0, i] == 1)
        //        {
        //            map[0, i] = 3;
        //        }

        //    }
        //    for (int i = 0; i < mSize; i++)
        //    {
        //        if (map[mSize - 1, i] == 1)
        //        {
        //            map[mSize - 1, i] = 2;
        //        }
        //    }
        //}
        //if (rn == 2)
        //{
        //    for (int i = 0; i < mSize; i++)
        //    {
        //        if (map[i, mSize - 1] == 1)
        //        {
        //            map[i, mSize - 1] = 3;
        //        }

        //    }
        //    for (int i = 0; i < mSize; i++)
        //    {
        //        if (map[i , 0] == 1)
        //        {
        //            map[i , 0] = 2;
        //        }
        //    }
        //}
        //if (rn == 3)
        //{
        //    for (int i = 0; i < mSize; i++)
        //    {
        //        if (map[mSize - 1, i] == 1)
        //        {
        //            map[mSize - 1, i] = 3;
        //        }

        //    }
        //    for (int i = 0; i < mSize; i++)
        //    {
        //        if (map[0, i] == 1)
        //        {
        //            map[0, i] = 2;
        //        }
        //    }
        //}



        //int changeLeft;
        //int changeup;
        //for (int i = 0; i < mSize; i++)
        //{
        //    for (int z = 0; z < mSize; z++)
        //    {

        //        newPosition[0] = moveRight * i;
        //        newPosition[1] = moveDown * z;
        //        changeLeft = mSize / 2;
        //        newPosition[0] += moveLeft * changeLeft;
        //        changeup = mSize / 2;
        //        newPosition[1] += moveUp * changeup;
        //        if (map[i, z] == 1)
        //        {
        //            if (i != changeLeft || z != changeup) { GameObject a = Instantiate(norRoom, newPosition, newRotation); a.transform.parent = gameObject.transform; };
        //        }
        //        if (map[i, z] == 2)
        //        {
        //            if (i != changeLeft || z != changeup) { GameObject a = Instantiate(iteRoom, newPosition, newRotation); a.transform.parent = gameObject.transform; };
        //        }
        //        if (map[i, z] == 3)
        //        {
        //            if (i != changeLeft || z != changeup) { GameObject a = Instantiate(endRoom, newPosition, newRotation); a.transform.parent = gameObject.transform; };
        //        }
        //    }
        //}

    }
    static int[,] GetVeinDown(int[,] r, int mSize)
    {
        int down = mSize / 2;
        int vein = mSize / 2;
        int[,] map = r;
        int rn;
        int f = mSize / 2;
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
    static int[,] GetVeinUp(int[,] r, int mSize)
    {
        int up = mSize / 2;
        int vein = mSize / 2;
        int[,] map = r;
        int rn;
        int f = mSize / 2;
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
    static int[,] GetVeinRight(int[,] r, int mSize)
    {
        int right = mSize / 2;
        int vein = mSize / 2;
        int[,] map = r;
        int rn;
        int f = mSize / 2;
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
    static int[,] GetVeinLeft(int[,] r, int mSize)
    {
        int left = mSize / 2;
        int vein = mSize / 2;
        int[,] map = r;
        int rn;
        int f = mSize / 2;
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
