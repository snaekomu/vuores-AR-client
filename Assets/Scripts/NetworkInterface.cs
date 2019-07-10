using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkInterface : MonoBehaviour
{
    private const string address = "http://vuores.snaekomu.xyz";
    private const string version = "v1";

    //------------------------------
    //HELPERS
    //------------------------------

    // Request helper builders

    public static string Request(string route, params string[] query)
    {
        if (query.Length % 2 != 0) throw new System.ArgumentException("Query needs even parameters");
        string[] q = new string[query.Length/2];
        for (int i = 0; i < q.Length; i++)
        {
            q[i] = query[i*2] + "=" + query[i*2+1];
        }
        return Request(route, string.Join("&", q));
    }

    public static string Request(string route, string query)
    {
        
        if (route == "api") route += "/" + version;
        return string.Format("{0}/{1}?{2}", address, route, query);
    }

    public static string Request(string route)
    {
        
        if (route == "api") route += "/" + version;
        return string.Format("{0}/{1}/", address, route);
    }

    //JSON helper function

    public T ParseJSON<T>(string s)
    {
        Debug.Log(s);
        return JsonUtility.FromJson<T>(s);
    }

    //------------------------------
    //ACCESSIBLE FUNCTIONS
    //------------------------------

    public void Get<T>(string req, Action<T> Callback)
    {
        Debug.Log(string.Format("Requesting URI: {0}", req));
        StartCoroutine(GetRoutine<T>(req, Callback));
    }

    public void Get<T>(string req, Action<Texture2D> Callback)
    {
        StartCoroutine(GetRoutine<T>(req, Callback));
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
