using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingFirstScenes : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        LoadScene("Lobby");
        LoadScene("Door");
        LoadScene("Door2");
    }

    private void LoadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);
    }
}
