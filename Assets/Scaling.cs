using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    bool fullscreenOn = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(640, 360, false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.F11)) && !fullscreenOn)
        {
            Screen.SetResolution(Screen.height, Screen.width, true);
            fullscreenOn = true;
        }
        if ((Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.F11)) && fullscreenOn)
        {
            Screen.SetResolution(640, 360, false);
            fullscreenOn = false;
        }
    }
}
