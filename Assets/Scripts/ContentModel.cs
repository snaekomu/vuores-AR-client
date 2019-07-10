using UnityEngine;

[System.Serializable]
public class ContentModel
{
    public int order;
    public string type;
    public string url;
    public string desc;

    public static ContentModel FromJson(string jsonString)
    {
        return JsonUtility.FromJson<ContentModel>(jsonString);
    }

    [System.Serializable]
    public class Array
    {
        public ContentModel[] elements;
    }
}
