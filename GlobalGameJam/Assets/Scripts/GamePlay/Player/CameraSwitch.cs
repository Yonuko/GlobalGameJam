using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject cameraOne;
    public GameObject cameraTwo;
    public GameObject switchTransitionUI;

    public GameObject playerOne;
    public GameObject playerTwo;

    AudioListener audioOne;
    AudioListener audioTwo;

    private float startTime = 0f;
    public float holdTime = 1.0f;       // Temps en seconde à tenir pour faire le switch de camera

    private bool cameraSwitch = false;
    public bool isFishHome = false;     // Si true, le poisson est dans la main du perso et la camera ne peut pas switch

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        audioOne = cameraOne.GetComponent<AudioListener>();
        audioTwo = cameraTwo.GetComponent<AudioListener>();

        audioTwo.enabled = false;
        cameraTwo.GetComponent<Camera>().enabled = false;
        playerTwo.GetComponent<PlayerMouvement>().stopMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFishHome){
            startTime = Time.time;
        }

        if (Input.GetMouseButton(0) && !isFishHome) // Changement de cam
        {
            if (startTime + holdTime <= Time.time)
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
        if (cameraSwitch)
        {
            audioTwo.enabled = false;
            cameraTwo.GetComponent<Camera>().enabled = false;
            playerTwo.GetComponent<PlayerMouvement>().stopMoving = true;

            audioOne.enabled = true;
            cameraOne.GetComponent<Camera>().enabled = true;
            playerOne.GetComponent<PlayerMouvement>().stopMoving = false;

            //playerTwo.tag = "Untagged";
            //playerOne.tag = "Player";
            

            cameraSwitch = false;
        }
        else
        {
            audioTwo.enabled = true;
            cameraTwo.GetComponent<Camera>().enabled = true;
            playerTwo.GetComponent<PlayerMouvement>().stopMoving = false;

            audioOne.enabled = false;
            cameraOne.GetComponent<Camera>().enabled = false;
            playerOne.GetComponent<PlayerMouvement>().stopMoving = true;

            //playerOne.tag = "Untagged";
            //playerTwo.tag = "Player";

            cameraSwitch = true;
        }
    }

    IEnumerator delaySwitch()
    {
        yield return new WaitForSeconds(0.25f);
        cameraChange();
    }

    public bool getPlayer()
    {
        return !this.cameraSwitch;
    }
}
