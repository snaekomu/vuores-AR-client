using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private NetworkInterface networkInterface;
    [SerializeField] private Database database;
    
    public void Start()
    {
        GetDbInf();
    }

    void GetDbInf()
    {
        networkInterface.Get("localhost:3000/api/v1/dbinfo/", SaveDbInf);
    }

    void SaveDbInf(string res)
    {
        database.SetDatabaseInfo(JsonUtility.FromJson<DatabaseInfo>(res));
        ReadDatabase();
    }

    void ReadDatabase()
    {
        for (int i = 0; i < database.DatabaseInfo.length; i++)
        {
            networkInterface.Get("localhost:3000/api/v1/id." + i.ToString(), SaveDatabaseEntry);
        }
    }

    void SaveDatabaseEntry(string res)
    {
        if (database.DatabaseEntries.Count < database.DatabaseInfo.length)
        {
            database.AddDBEntry(JsonUtility.FromJson<DatabaseEntry>(res));
        }
        else if (database.DatabaseEntries.Count == database.DatabaseInfo.length)
        {
            database.AddDBEntry(JsonUtility.FromJson<DatabaseEntry>(res));
            GetTextures();
        }
    }

    void GetTextures()
    {
        for (int i = 0; i < database.DatabaseEntries.Count; i++)
        {
            if (i < database.DatabaseEntries.Count)
            {
                networkInterface.Get(database.DatabaseEntries[i].url);
            }
            else if (i < database.DatabaseEntries.Count)
            {
                database.DatabaseEntries[i].GetTexture(networkInterface);
            }
        }
    }
}
