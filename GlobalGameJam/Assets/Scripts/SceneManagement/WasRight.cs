using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasRight : MonoBehaviour
{
    private void OnTriggerExit (Collider other)
    {
        transform.parent.GetComponent<MeIsDoor>().lastSeenSetter(true);
    }
}
