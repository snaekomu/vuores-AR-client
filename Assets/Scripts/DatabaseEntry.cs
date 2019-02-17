using UnityEngine;

[System.Serializable]
public class DatabaseEntry
{
    public int id;
    public string url;
    public string text;
    public Texture2D texture;
    
    public static DatabaseEntry CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<DatabaseEntry>(jsonString);
    }

    public void GetTexture(NetworkInterface net)
    {
        net.Get(url, SaveTexture);
    }

    public void SaveTexture(Texture2D texture)
    {
        this.texture = texture;
    }
}
