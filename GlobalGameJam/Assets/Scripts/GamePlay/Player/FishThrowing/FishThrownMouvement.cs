using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishThrownMouvement : MonoBehaviour
{

    public float speed;
    public Vector3 destination;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(destination);

        if (Vector3.Distance(transform.position, destination) <= 0.2f)
        {
            Destroy(this);
            return;
        }
        transform.Translate(new Vector3(0, 0, 1 * speed));
    }
}
