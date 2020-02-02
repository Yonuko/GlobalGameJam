using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public GameObject switchTransitionUI;

    public GameObject player;
    public GameObject saulmon;

    Camera mainCamera;

    private float startTime = 0f;
    public float holdTime = 0.2f;       // Temps en seconde à tenir pour faire le switch de camera

    private bool cameraSwitch = true, isFishHome = true;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        player = GameObject.FindWithTag("Player");

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !isFishHome){
            startTime = Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && !isFishHome) // Changement de cam
        {
            if (startTime <= holdTime)
            {

                switchTransitionUI.GetComponent<Animator>().Play("FadeIn", 0, 0);
                StartCoroutine(delaySwitch());

                startTime = Time.time;
            }
        }

        if( Input.GetMouseButtonUp(0) && !isFishHome)
        {
            startTime = 0f;
        }
    }

    private void cameraChange()
    {
        // Poisson
        saulmon.GetComponent<FishMouvement>().enabled = cameraSwitch;
        if (saulmon.GetComponent<FishMouvement>().isOnWater && !cameraSwitch)
        {
            GameObject.Find("WaterTint").GetComponent<Image>().enabled = false;
        }
        // Player
        player.GetComponent<PlayerMouvement>().stopMoving = cameraSwitch;
        // Assigne les tag en fonction du personnage que l'on joue
        player.tag = (cameraSwitch) ? "Untagged" : "Player";
        saulmon.tag = (cameraSwitch) ? "Player" : "Fish";
        Camera.main.GetComponent<CameraController>().ChangePlayer();
        if (cameraSwitch)
        {
            saulmon.GetComponent<Animator>().Play("Nage", 0, 0);
        }
        cameraSwitch = !cameraSwitch;
    }

    IEnumerator delaySwitch()
    {
        yield return new WaitForSeconds(0.25f);
        cameraChange();
    }

    public bool amIThePlayer()
    {
        return !this.cameraSwitch;
    }

    public void FishCameBack(bool isHeHome)
    {
        isFishHome = isHeHome;
    }
}
