using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Database : ScriptableObject
{
    public DatabaseInfo DatabaseInfo { get; private set; }
    public List<DatabaseEntry> DatabaseEntries { get; private set; }

    private void InitList()
    {
        DatabaseEntries = new List<DatabaseEntry>();
    }

    public void AddDBEntry(DatabaseEntry entry)
    {
        if (DatabaseEntries == null)
        {
            InitList();
        }

        DatabaseEntries.Add(entry);
    }

    public void SetDB(List<DatabaseEntry> list)
    {
        if (DatabaseEntries == null)
        {
            InitList();
        }

        DatabaseEntries = list;
    }

    public void SetDatabaseInfo(DatabaseInfo dbinf)
    {
        DatabaseInfo = dbinf;
    }

    public void GetDBInf(NetworkInterface net)
    {
        Debug.Log("Getting database information...");
        net.Get("localhost:3000/api/v1/dbinfo/", SaveDBInf);
    }

    private void SaveDBInf(string res)
    {
        SetDatabaseInfo(JsonUtility.FromJson<DatabaseInfo>(res));
        UpdatableImage.SetAvailableImages(DatabaseInfo.length);
        Debug.Log("Database information saved.");
        Main.self.Next();
    }
    
    public void ReadDatabase(NetworkInterface net)
    {
        Debug.Log("Getting database entries...");
        for (int i = 0; i < DatabaseInfo.length; i++)
        {
            net.Get("localhost:3000/api/v1/id." + i.ToString(), SaveDatabaseEntry);
        }
    }
    
    void SaveDatabaseEntry(string res)
    {
        if (DatabaseEntries.Count < DatabaseInfo.length)
        {
            DatabaseEntry e = JsonUtility.FromJson<DatabaseEntry>(res);
            AddDBEntry(e);
            Debug.Log("Entry " + e.id.ToString() + " saved.");
        }
        else if (DatabaseEntries.Count == DatabaseInfo.length)
        {
            DatabaseEntry e = JsonUtility.FromJson<DatabaseEntry>(res);
            AddDBEntry(e);
            Debug.Log("Entry " + e.id.ToString() + " saved. Last entry saved.");
            Main.self.Next();
        }
    }
}
