using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Fish")
        {
            GetComponent<FishThrowing>().PickUpFish();
            GameObject.Find("GameController").GetComponent<CameraSwitch>().FishCameBack(true);
            Destroy(hit.gameObject);
        }        
    }
}
