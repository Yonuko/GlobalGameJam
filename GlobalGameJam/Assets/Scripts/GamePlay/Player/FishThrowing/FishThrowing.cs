using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishThrowing : MonoBehaviour
{

    public GameObject aimingPosition, fishThrownPointPrefab;
    bool hasTheFish = true, shoudRealese, aiming;

    GameObject targetHitObject = null;

    Coroutine AlreadyAimed = null;

    // Start is called before the first frame update
    void Start()
    {
        
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
            // temp ----
            GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            temp.transform.position = aimingPosition.transform.position;
            temp.GetComponent<Renderer>().material.color = Color.blue;
            temp.AddComponent<FishThrownMouvement>();
            temp.GetComponent<FishThrownMouvement>().speed = 1;
            temp.GetComponent<FishThrownMouvement>().destination = targetHitObject.transform.position;
            // ----
           // hasTheFish = false;
        }

        if (aiming)
        {
            if (targetHitObject == null)
            {
                targetHitObject = Instantiate(fishThrownPointPrefab);
            }
            Camera mainCamera = Camera.main;
            mainCamera.GetComponent<CameraController>().SetCameraAiming(true);
            mainCamera.fieldOfView = 45;
            mainCamera.transform.position = aimingPosition.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                targetHitObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - 2);
                targetHitObject.transform.LookAt(hit.normal);
        /*        float angleCorrection = Vector3.Angle(-targetHitObject.transform.rotation.eulerAngles, hit.point);
                Quaternion targetHitRotation = targetHitObject.transform.rotation;
                targetHitObject.transform.rotation = Quaternion.Euler(targetHitRotation.x + angleCorrection, targetHitRotation.y, targetHitRotation.z);
                Debug.DrawRay(hit.point, hit.transform.forward, Color.blue);
                Debug.DrawRay(hit.point, hit.transform.up, Color.green);
                Debug.DrawRay(hit.point, hit.transform.right, Color.red);*/
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

    public bool hasNoFish()
    {
        return hasTheFish;
    }
}
