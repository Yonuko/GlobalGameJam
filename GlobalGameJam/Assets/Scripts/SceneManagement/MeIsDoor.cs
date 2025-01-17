﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeIsDoor : MonoBehaviour
{
    Animator anim;

    

    bool lastSeen = false; // false = left, true = right

    public string nameSceneLeft;
    public string nameSceneRight;

    public bool isOpenable = true;
    public bool ignoreLoader = true;

    private bool outOfTrigger = true;
    private bool animationEnded = false;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if(outOfTrigger  && animationEnded)
        {
            animationEnded = false;
            anim.Play("SlideDownDoorFinal");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(Loader.get().datas.keys["Action"]) && isOpenable)
            {
                LoadScene(nameSceneRight);
                LoadScene(nameSceneLeft);
                anim.Play("SlideUpDoorFinal"); // && isHoldingFish
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            outOfTrigger = false;
            LoadScene(nameSceneRight);
            LoadScene(nameSceneLeft);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            outOfTrigger = true;
        }
    }

    public void ExitDoor()
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

    private void LoadScene(string sceneName)
    {
        if (!this.ignoreLoader)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    private void UnloadScene(string sceneName)
    {
        if (!this.ignoreLoader)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
                SceneManager.UnloadSceneAsync(sceneName);
        }
    }

    public void lastSeenSetter(bool lastSeen)
    {
        this.lastSeen = lastSeen;
    }
    public void animSetter(bool laboule)
    {
        this.animationEnded = laboule;
    }
}
