using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkInterface : MonoBehaviour
{
    public const string ip = "localhost:3000";
    public const string version = "v1";
    public const string sqldb = "elements";

    public string Req(string ip, string version, string task, string arguments = "")
    {
        return ip + "/api/" + sqldb + "/" + task + "/" + arguments + "/";
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
            Debug.Log(uwr.error);
        }
        else
        {
            Callback(DownloadHandlerTexture.GetContent(uwr));
        }
    }
}
