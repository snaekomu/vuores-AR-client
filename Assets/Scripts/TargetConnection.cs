using System;
using UnityEngine;
using Vuforia;

public class TargetConnection : MonoBehaviour
{   
    private string id;
    private Query query;
    private NetworkInterface net;

    public void Awake() {
        id = "a";
        query.Add("target", id);
    }

    public void Start() {
        net = NetworkInterface.instance;
        DownloadImages();
    }

    private void DownloadImages() {
        net.Get(query.Url, SpawnImages);
    }

    private void SpawnImages(string s) {
        
    }
}
