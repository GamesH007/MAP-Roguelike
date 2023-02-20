using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameObject Player;
    PlayerController PlayerController;

    private void Start()
    {
        Player = GameObject.Find("Player");
        PlayerController = Player.GetComponent<PlayerController>();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        PlayerController.disabled = 0;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
