using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    //private const int dbVersion;
    private const int apiVersion = 1;
    [SerializeField] private NetworkInterface net;
    [SerializeField] private PrefabFactory factory;
    private DatabaseInfo dbInf;
    private string savePath;
    private List<DatabaseEntry> db = new List<DatabaseEntry>();
    
    //Retrieve database from server
    void Start()
    {
        //Read saved database version
        //Compare to online database
        //Sync if necessary
        //Load saved database
        savePath = Path.Combine(Application.persistentDataPath, "saveFile");
        Debug.Log(NetworkInterface.DBInf.length);
        GetDB();
    }

    
    //Loop of GET requests for the whole database
    void GetDB()    
    {
        for (int i = 0; i < NetworkInterface.DBInf.length; i++)
        {
            net.Get("localhost:3000/api/v" + apiVersion.ToString() + "/id." + i.ToString(), ParseDB);
        }
    }
    
    
    //Callback for database entry GET requests
    void ParseDB(string res)
    {
        Debug.Log(res);
        db.Add(JsonUtility.FromJson<DatabaseEntry>(res));
        if (db.Count() == NetworkInterface.DBInf.length)
        {
            SpawnPrefabs();
        }
    }

    
    //Loop to spawn prefabs based on database entries
    void SpawnPrefabs()
    {
        Debug.Log("spawning prefabs");
        for (int i = 0; i < db.Count; i++)
        {
            DatabaseEntry e = db[i];
            Transform t = factory.Get(0);
            t.position = new Vector3(e.posX, e.posY, e.posZ);
            t.eulerAngles = new Vector3(e.rotX, e.rotY, e.rotZ);
            t.localScale = new Vector3(e.scaleX, e.scaleY, e.scaleZ);
            t.gameObject.SetActive(true);
        }
    }
    
    //TODO:
    //Make stuff a long string of callbacks/deal with the whole knowing database length
    //Save to and read from local storage
}
