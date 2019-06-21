using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityEngine;
using UnityEngine.UI;

public class ImageConnection : Image
{   
    private string name;
    private NetworkInterface net;

    public void Start(){
        net = NetworkInterface.instance;
    }

    //Texture download handler
    public void DownloadTexture(url)
    {
        Debug.Log("Downloading images...");
        if (AvailableImages == null || AvailableImages.Count == 0)
        {
            SetAvailableImages(GameObject.FindObjectsOfType<UpdatableImage>().Length);
        }

        String g = NetworkInterface.ip + "/static/image/" + name;
        net.Get(g, AssignSprite);
    }

    public void AssignSprite(Texture2D tex)
    {
        Sprite s = Sprite.Create(tex, new Rect(0f,0f,(float)tex.width,(float)tex.height), new Vector2(0.5f, 0.5f));
        this.sprite = s;
        Debug.Log("Image saved.");
    }
}
