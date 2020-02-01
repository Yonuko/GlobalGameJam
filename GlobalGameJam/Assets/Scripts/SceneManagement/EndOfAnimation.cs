using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfAnimation : MonoBehaviour
{
    public void NTM()
    {
        transform.parent.GetComponent<MeIsDoor>().ExitDoor();
    }
}
