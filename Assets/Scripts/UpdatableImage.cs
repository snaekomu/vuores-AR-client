using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatableImage : Image
{
    public static List<Sprite> AvailableImages;
    private Sprite sprite;
    [SerializeField] private Vector3 scale;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetRandomSprite();
        this.sprite = sprite;
        this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sprite.texture.width);
        this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sprite.texture.height);
        this.rectTransform.localScale = scale;
    }

    public static Sprite GetRandomSprite()
    {
        int r = Random.Range(0, AvailableImages.Count);
        Sprite s = AvailableImages[r];
        AvailableImages.RemoveAt(r);
        return s;
    }
}
