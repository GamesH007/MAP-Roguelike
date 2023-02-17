using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    bool fullOn = false;
    public TextMeshProUGUI text;

    public void Fullscreen()
    {
        if (!fullOn)
        {
            Screen.SetResolution(1920, 1080, true);
            fullOn = true;
            text.text = "Fullscreen: ON";
        }
        else
        {
            Screen.SetResolution(960, 540, false);
            fullOn = false;
            text.text = "Fullscreen: OFF";
        }
    }
    
}
