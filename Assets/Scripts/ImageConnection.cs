using UnityEngine;
using UnityEngine.UI;

public class ImageConnection : MonoBehaviour
{
    Image image;

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    //Texture download handler
    public void DownloadTexture(NetworkInterface net, string uri)
    {
        net.Get<Texture2D>(uri, AssignSprite);
    }

    public void AssignSprite(Texture2D tex)
    {
        Sprite s = Sprite.Create(tex, new Rect(0f,0f,(float)tex.width,(float)tex.height), new Vector2(0.5f, 0.5f));
        image.sprite = s;
        Debug.Log("Image saved.");
    }
}
