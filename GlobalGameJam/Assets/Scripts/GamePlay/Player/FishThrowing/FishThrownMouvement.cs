using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishThrownMouvement : MonoBehaviour
{

    public float speed;
    public Vector3 destination;

    private void Start()
    {
        StartCoroutine(WaitBeforeDelete());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(destination);

        if (Vector3.Distance(transform.position, destination) <= 0.2f)
        {
            Destroy(this);
            tag = "Fish";
            GameObject.Find("GameController").GetComponent<CameraSwitch>().FishCameBack(false);
            return;
        }
        transform.Translate(new Vector3(0, 0, 1 * speed));
    }

    IEnumerator WaitBeforeDelete()
    {
        yield return new WaitForSeconds(3);
        GameObject.FindWithTag("Player").GetComponent<FishThrowing>().PickUpFish();
        GameObject.Find("GameController").GetComponent<CameraSwitch>().FishCameBack(true);
        Destroy(gameObject);
    }
}
