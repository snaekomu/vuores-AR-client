using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkInterface : MonoBehaviour
{
    public const string ip = "http://vuores.snaekomu.xyz";
    public const string version = "v1";
    public const string dbName = "ar_api";

    public static NetworkInterface instance;

    public void Awake() {
        instance = this;
    }

    public string Req(string ip, string version, string query)
    {
        return ip + "/api/" + version + "/" + query;
    }

    public string Query(Dictionary<string, string> fields)
    {
        string o = "?";

        foreach (var pair in fields)
        {
            o += pair.Key + "=" + pair.Value + "&";
        }

        return o;
    }
    
    public void Get(string req, Action<String> Callback)
    {
        StartCoroutine(GetRoutine(req, Callback));
    }
    
    public void Get(string req, Action<Texture2D> Callback)
    {
        StartCoroutine(GetRoutine(req, Callback));
    }
    
    private IEnumerator GetRoutine(string req, Action<String> Callback)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(req);
        yield return uwr.SendWebRequest();
 
        if(uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Callback(uwr.downloadHandler.text);
        }
        
    }

    private IEnumerator GetRoutine(string req, Action<Texture2D> Callback)
    {
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(req);
        yield return uwr.SendWebRequest();
 
        if(uwr.isNetworkError || uwr.isHttpError)
        {
            
        }
        else
        {
            Texture2D t = DownloadHandlerTexture.GetContent(uwr);
            if (t == null)
            {
                Debug.Log("bad error texture null what happened");
            }
            else
            {
                Callback(t);
            }
        }
    }
}
