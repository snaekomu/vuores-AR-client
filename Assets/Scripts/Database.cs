using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Database : ScriptableObject
{
    public DatabaseMeta DatabaseMeta { get; private set; }
    public DatabaseEntry[] DatabaseEntries { get; private set; }

    private void InitArray(int length)
    {
        DatabaseEntries = new DatabaseEntry[length];
    }

    public void AddDBEntry(int i, DatabaseEntry entry)
    {
        if (DatabaseEntries == null)
        {
            InitArray(DatabaseMeta.length);
        }

        DatabaseEntries[i] = entry;
    }

    public void SetDB(DatabaseEntry[] arr)
    {
        if (DatabaseEntries == null)
        {
            InitArray(DatabaseMeta.length);
        }

        DatabaseEntries = arr;
    }

    public void SetDatabaseInfo(DatabaseMeta dbmeta)
    {
        DatabaseMeta = dbmeta;
        InitArray(DatabaseMeta.length);
    }

    public void GetDBMeta(NetworkInterface net)
    {
        Debug.Log("Getting database information...");
        net.Get(net.Req(NetworkInterface.ip, NetworkInterface.version, "meta", "all"), SaveDBMeta);
    }

    private void SaveDBMeta(string res)
    {
        SetDatabaseInfo(JsonUtility.FromJson<DatabaseMeta>(res));
        //UpdatableImage.SetAvailableImages(DatabaseMeta.length);
        Debug.Log("Database information saved.");
        Main.Next();
    }
    
    public void ReadDatabase(NetworkInterface net)
    {
        Debug.Log("Getting database entries...");
        for (int i = 0; i < DatabaseMeta.idArray.Length; i++)
        {
            net.Get(net.Req(NetworkInterface.ip, NetworkInterface.version, "select", "id=" + DatabaseMeta.idArray[i].ToString()), SaveDatabaseEntry);
        }
    }
    
    void SaveDatabaseEntry(string res)
    {
        DatabaseEntry e = JsonUtility.FromJson<DatabaseEntry>(res);
        int i = Array.IndexOf(DatabaseMeta.idArray, e.id);
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
