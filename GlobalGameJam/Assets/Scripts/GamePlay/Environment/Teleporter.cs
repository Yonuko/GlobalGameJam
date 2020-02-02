using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject destination;
    public bool isActive = true;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetKeyDown(Loader.get().datas.keys["Action"]) && isActive)
        {
            other.transform.position = destination.transform.position + new Vector3(0,1,0);
        }
    }
}
