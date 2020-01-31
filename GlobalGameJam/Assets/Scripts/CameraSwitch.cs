using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject cameraOne;
    public GameObject cameraTwo;
    public GameObject switchTransitionUI;

    AudioListener audioOne;
    AudioListener audioTwo;

    private float startTime = 0f;
    public float holdTime = 2.0f;       // Temps en seconde à tenir pour faire le switch de camera

    private bool cameraSwitch = false;
    public bool isFishHome = false;     // Si true, le poisson est dans la main du perso et la camera ne peut pas switch

    // Start is called before the first frame update
    void Start()
    {
        audioOne = cameraOne.GetComponent<AudioListener>();
        audioTwo = cameraTwo.GetComponent<AudioListener>();

        audioTwo.enabled = false;
        cameraTwo.GetComponent<Camera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isFishHome){
            Debug.Log("Starting switch Timer");
            startTime = Time.time;
        }

        if (Input.GetMouseButton(1) && !isFishHome) // Changement de cam
        {
            if (startTime + holdTime <= Time.time)
            {
                Debug.Log("Finishing switch Timer");

                switchTransitionUI.GetComponent<Animator>().Play("FadeIn", 0, 0);
                StartCoroutine(delaySwitch());

                startTime = Time.time;
            }
        }

        if( Input.GetMouseButtonUp(1) && !isFishHome)
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
            audioOne.enabled = true;
            cameraOne.GetComponent<Camera>().enabled = true;

            cameraSwitch = false;
        }
        else
        {
            audioTwo.enabled = true;
            cameraTwo.GetComponent<Camera>().enabled = true;
            audioOne.enabled = false;
            cameraOne.GetComponent<Camera>().enabled = false;

            cameraSwitch = true;
        }
    }

    IEnumerator delaySwitch()
    {
        yield return new WaitForSeconds(0.25f);
        cameraChange();
    }
}
