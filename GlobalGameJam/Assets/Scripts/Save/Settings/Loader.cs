using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

    public const string saveFileName = "gameSave.save";

    static Loader Instance;

    public Datas datas;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
            datas = new Datas();
        }
        else
        {
            Destroy(gameObject);
        }

        if (!File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            DefaultDatas();
        }
        else
        {
            Load();
        }
    }
	
    void DefaultDatas()
    {
        datas.keys["Jump"] = KeyCode.Space;
        datas.keys["Escape"] = KeyCode.Escape;
        datas.keys["Sprint"] = KeyCode.LeftShift;
        datas.keys["Action"] = KeyCode.E;
        datas.keys["Monter (poisson)"] = KeyCode.Space;
        datas.keys["Descendre (poisson)"] = KeyCode.LeftShift;

        DataManager.Save(datas, saveFileName);
        Load();
    }
	
	public void Save() {
        DataManager.Save(datas,saveFileName);
    }

    public void SaveKeys()
    {
        datas.keys = KeysManager.keys;
    }

    public void Load()
    {
        datas = (Datas)DataManager.Load(saveFileName);
    }

    public static bool IsLoader()
    {
        if (GameObject.FindWithTag("Loader") && File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            return true;
        }
        else if(!File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            Debug.LogError("File doesn't exist");
            return false;
        }
        else
        {
            Debug.LogError("Loader not found");
            return false;
        }
    }

    public static Loader get()
    {
        return Instance;
    }
}
