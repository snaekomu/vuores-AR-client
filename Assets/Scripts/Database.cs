using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Database : ScriptableObject
{
    public DatabaseInfo DatabaseInfo { get; private set; }
    public DatabaseEntry[] DatabaseEntries { get; private set; }

    private void InitArray(int length)
    {
        DatabaseEntries = new DatabaseEntry[length];
    }

    public void AddDBEntry(int i, DatabaseEntry entry)
    {
        if (DatabaseEntries == null)
        {
            InitArray(DatabaseInfo.length);
        }

        DatabaseEntries[i] = entry;
    }

    public void SetDB(DatabaseEntry[] arr)
    {
        if (DatabaseEntries == null)
        {
            InitArray(DatabaseInfo.length);
        }

        DatabaseEntries = arr;
    }

    public void SetDatabaseInfo(DatabaseInfo dbinf)
    {
        DatabaseInfo = dbinf;
        InitArray(DatabaseInfo.length);
    }

    public void GetDBInf(NetworkInterface net)
    {
        Debug.Log("Getting database information...");
        net.Get(net.Req(NetworkInterface.ip, NetworkInterface.version, "dbinfo"), SaveDBInf);
    }

    private void SaveDBInf(string res)
    {
        SetDatabaseInfo(JsonUtility.FromJson<DatabaseInfo>(res));
        //UpdatableImage.SetAvailableImages(DatabaseInfo.length);
        Debug.Log("Database information saved.");
        Main.Next();
    }
    
    public void ReadDatabase(NetworkInterface net)
    {
        Debug.Log("Getting database entries...");
        for (int i = 0; i < DatabaseInfo.idArray.Length; i++)
        {
            net.Get(net.Req(NetworkInterface.ip, NetworkInterface.version, "select", "id=" + DatabaseInfo.idArray[i].ToString()), SaveDatabaseEntry);
        }
    }
    
    void SaveDatabaseEntry(string res)
    {
        DatabaseEntry e = JsonUtility.FromJson<DatabaseEntry>(res);
        int i = Array.IndexOf(DatabaseInfo.idArray, e.id);
        AddDBEntry(i,e);
        if (Array.Exists(DatabaseEntries, element => element == null))
        {   
            Debug.Log("Entry " + e.id.ToString() + " saved.");
        }
        else
        {
            Debug.Log("Entry " + e.id.ToString() + " saved. Last entry saved.");
            Main.Next();
        }
    }
}
