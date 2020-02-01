using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LauncheGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Option()
    {
        SceneManager.LoadScene("KeyBindingScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
