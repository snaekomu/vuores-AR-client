using UnityEngine;

[System.Serializable]
public class DatabaseEntry
{
    public int id;
    
    public float posX;
    public float posY;
    public float posZ;

    public float rotX;
    public float rotY;
    public float rotZ;

    public float scaleX;
    public float scaleY;
    public float scaleZ;

    public string content;
    
    public static DatabaseEntry CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<DatabaseEntry>(jsonString);
    }
}
