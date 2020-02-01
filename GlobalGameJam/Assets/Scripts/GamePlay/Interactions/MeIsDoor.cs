using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeIsDoor : MonoBehaviour
{
    Animator anim;

    

    bool lastSeen = false; // false = left, true = right

    public string nameSceneLeft;
    public string nameSceneRight;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LoadScene(nameSceneRight);
            LoadScene(nameSceneLeft);
            if (Input.GetKeyDown("e"))
            {
                anim.Play("SlideUpDoor"); // && isHoldingFish
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (lastSeen)
            {
                UnloadScene(nameSceneLeft);
            }
            else
            {
                UnloadScene(nameSceneRight);
            }
        }
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

    public void lastSeenSetter(bool lastSeen)
    {
        this.lastSeen = lastSeen;
    }
}
