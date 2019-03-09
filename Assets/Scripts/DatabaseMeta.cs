using UnityEngine;

[System.Serializable]
public class DatabaseMeta
{
    public int revision;
    public int length;
    public int[] idArray;
        
    public static DatabaseMeta CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<DatabaseMeta>(jsonString);
    }
}
