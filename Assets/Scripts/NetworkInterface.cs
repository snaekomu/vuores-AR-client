using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkInterface : MonoBehaviour
{
    private string res;
    public static DatabaseInfo DBInf { get; private set; }

    public delegate void Callback(string res);
    
    //Check and preload database info when awaken
    private void Awake()
    {
        if (DatabaseInfo == null)
        {
            GetDBInf();
        }
    }

    // Get database info (version and length)
    private void GetDBInf()
    {
        Get("localhost:3000/api/v1/dbinfo", SetDBInf);
    }
    
    //Parse and save Database Info
    private void SetDBInf(string res)
    {
        DBInf = JsonUtility.FromJson<DatabaseInfo>(res);
        Debug.Log(res);
        Debug.Log(DBInf.length);
        //If request works
        //Main.GetDB();
        //Else
        //Main.TellPlayerToFuckOff();
    }
    
    //Generic GET request with callback
    //Starts GET coroutine
    public void Get(string req, Callback callback)
    {
        StartCoroutine(SendRequest(req, callback));
    }
    
    //GET request coroutine
    IEnumerator SendRequest(string req, Callback callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(req);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            // Or retrieve results as binary data
            res = www.downloadHandler.text;
            callback(res);
        }
    }
}
