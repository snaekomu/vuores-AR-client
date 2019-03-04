using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatableImage : Image
{
    public static List<int> AvailableImages = new List<int>();
    [SerializeField] private Vector3 scale = new Vector3(1f, -1f, 1f);

    //Set the correct scale on start
    private void Start()
    {
        //GetComponent<RectTransform>().localScale = scale;
    }

    //Set the list of available images
    public static void SetAvailableImages(int l)
    {
        for (int i = 0; i < l; i++)
        {
            AvailableImages.Add(i);
        }
    }
    
    //Get a random entry from the available images list
    private int GetRandomEntry()
    {
        int r = Random.Range(0, AvailableImages.Count);
        int i = AvailableImages[r];
        AvailableImages.RemoveAt(r);
        return i;
    }

    //Texture download handler
    public void DownloadTexture(NetworkInterface net, Database db)
    {
        Debug.Log("Downloading images...");
        if (AvailableImages == null || AvailableImages.Count == 0)
        {
            SetAvailableImages(GameObject.FindObjectsOfType<UpdatableImage>().Length);
        }
        net.Get(NetworkInterface.ip + "/" + db.DatabaseEntries[GetRandomEntry()].url, AssignSprite);
    }

    public void AssignSprite(Texture2D tex)
    {
        Sprite s = Sprite.Create(tex, new Rect(0f,0f,(float)tex.width,(float)tex.height), new Vector2(0.5f, 0.5f));
        this.sprite = s;
        Debug.Log("Image saved.");
    }
}
