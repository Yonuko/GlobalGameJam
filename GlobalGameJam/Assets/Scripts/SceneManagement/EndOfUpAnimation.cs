using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfUpAnimation : MonoBehaviour
{
    public void CMOI()
    {
        transform.parent.GetComponent<MeIsDoor>().animSetter(true);
    }
}
