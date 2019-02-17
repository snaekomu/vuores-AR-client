using System;
using System.Collections;
using System.Collections.Generic;
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
        UnityWebRequest www = UnityWebRequest.Get(req);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Callback(www.downloadHandler.text);
        }
        
    }

    private IEnumerator GetRoutine(string req, Action<Texture2D> Callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(req);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Callback(((DownloadHandlerTexture)www.downloadHandler).texture);
        }
    }
}
