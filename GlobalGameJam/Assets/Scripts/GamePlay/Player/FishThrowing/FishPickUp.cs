using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider hit)
    {
        Debug.Log(hit.gameObject);
        if (hit.tag == "Fish")
        {
            Debug.Log("Yo");
            GetComponent<FishThrowing>().PickUpFish();
            Destroy(hit.gameObject);
        }        
    }
}
