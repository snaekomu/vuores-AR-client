using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkInterface : MonoBehaviour
{

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
