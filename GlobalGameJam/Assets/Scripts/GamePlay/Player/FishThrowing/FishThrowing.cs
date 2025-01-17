﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishThrowing : MonoBehaviour
{

    public GameObject aimingPosition, fishThrownPointPrefab, Fish;
    bool hasTheFish = true, shoudRealese, aiming;

    GameObject targetHitObject = null;

    Coroutine AlreadyAimed = null;

    Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && hasTheFish)
        {
            if (AlreadyAimed == null && !aiming)
            {
                StartCoroutine(WaitForUnleash());
            }
            aiming = true;
        }

        if (Input.GetMouseButtonUp(0) && aiming)
        {
            aiming = false;
            if (shoudRealese)
            {
                return;
            }
            // Launch the fish
            GameObject go = Instantiate(Fish, aimingPosition.transform.position, transform.rotation);
            go.GetComponent<FishThrownMouvement>().speed = 0.7f;
            go.GetComponent<FishThrownMouvement>().destination = targetHitObject.transform.position;
            go.GetComponent<Animator>().Play("Fly", 0, 0);
            GameObject.Find("GameController").GetComponent<CameraSwitch>().saulmon = go;
            hasTheFish = false;
        }

        if (aiming)
        {
            if (targetHitObject == null)
            {
                targetHitObject = Instantiate(fishThrownPointPrefab);
            }
            anim.Play("Throw", 0, 0.274f);
            Camera mainCamera = Camera.main;
            mainCamera.GetComponent<CameraController>().SetCameraAiming(true);
            mainCamera.fieldOfView = 45;
            mainCamera.transform.position = aimingPosition.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                targetHitObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - 2);
                targetHitObject.transform.LookAt(hit.normal);
             /*   float angleCorrection = Vector3.Angle(-targetHitObject.transform.rotation.eulerAngles, hit.point);
                Quaternion targetHitRotation = targetHitObject.transform.rotation;
                targetHitObject.transform.rotation = Quaternion.Euler(targetHitRotation.x + angleCorrection, targetHitRotation.y, targetHitRotation.z);*/
            }
        }
        else
        {
            StopAllCoroutines();
            AlreadyAimed = null;
            if (targetHitObject != null)
            {
                Destroy(targetHitObject);
                targetHitObject = null;
            }
            Camera mainCamera = Camera.main;
            mainCamera.fieldOfView = 60;
            mainCamera.GetComponent<CameraController>().SetCameraAiming(false);
        }
    }

    IEnumerator WaitForUnleash()
    {
        shoudRealese = true;
        yield return new WaitForSeconds(0.5f);
        shoudRealese = false;
        AlreadyAimed = null;
    }

    public bool isAiming()
    {
        return aiming;
    }

    public bool hasNoFish()
    {
        return hasTheFish;
    }

    public void PickUpFish()
    {
        if (!hasNoFish())
        {
            hasTheFish = true;
        }
    }
}
