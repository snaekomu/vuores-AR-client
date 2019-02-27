using UnityEngine;

[System.Serializable]
public class DatabaseEntry
{
    public int id;
    public string url;
    public string text;
    
    public static DatabaseEntry CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<DatabaseEntry>(jsonString);
    }
}
