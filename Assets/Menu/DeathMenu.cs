using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    GameObject Player;
    PlayerController Controller;

    private void Start()
    {
        Player = GameObject.Find("Player");
        Controller = Player.GetComponent<PlayerController>();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        Controller.disabled = 0;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Controller.disabled = 0;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
