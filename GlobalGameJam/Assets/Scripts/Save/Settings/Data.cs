using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Datas{

    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    public string sceneName;

    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
	
}
