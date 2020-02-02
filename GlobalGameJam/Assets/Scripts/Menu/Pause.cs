using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool pause = false;
    void Update()
    {
        if (Input.GetKeyDown(Loader.get().datas.keys["Escape"]))
        {
            PauseManager();
        }
    }

    public void PauseManager()
    {
        if (!pause)
        {
            pause = true;
            Time.timeScale = 0f;
            GameObject.Find("Pause").GetComponent<Canvas>().enabled = true;
        }
        else
        {
            pause = false;
            Time.timeScale = 1f;
            GameObject.Find("Pause").GetComponent<Canvas>().enabled = false;
        }
    }
}
