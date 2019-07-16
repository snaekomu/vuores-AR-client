using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkInterface : MonoBehaviour
{
    //JSON helper function
    private T ParseJSON<T>(string s)
    {
        Debug.Log(s);
        return JsonUtility.FromJson<T>(s);
    }

    //------------------------------
    //ACCESSIBLE FUNCTIONS
    //------------------------------

    public void Get<T>(string uri, Action<T> Callback)
    {
        Debug.Log(string.Format("Requesting URI: {0}", uri));
        StartCoroutine(GetRoutine<T>(uri, Callback));
    }

    public void Get<T>(string uri, Action<Texture2D> Callback)
    {
        StartCoroutine(GetRoutine<T>(uri, Callback));
    }

    //------------------------------
    //COROUTINES
    //------------------------------

    private IEnumerator GetRoutine<T>(string req, Action<Texture2D> Callback)
    {
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(req);
        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(uwr);
            Callback(tex);
        }
    }

    private IEnumerator GetRoutine<T>(string req, Action<T> Callback)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(req);
        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            T p = ParseJSON<T>(uwr.downloadHandler.text);
            Callback(p);
        }
    }
}
