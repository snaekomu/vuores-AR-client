using UnityEngine;

[System.Serializable]
public class DatabaseInfo
{
    public int revision;
    public int length;
    public int[] idArray;
        
    public static DatabaseInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<DatabaseInfo>(jsonString);
    }
}
