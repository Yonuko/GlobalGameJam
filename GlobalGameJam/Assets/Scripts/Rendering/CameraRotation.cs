﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0.3f, 0));
    }
}
