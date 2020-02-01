using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeCassable : MonoBehaviour
{
    Animator anim;


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "PlateformeCassable")
        {
            hit.gameObject.tag = "Untagged";
            anim = hit.gameObject.GetComponent<Animator>();
            anim.Play("crumble");
        }
    }
}
