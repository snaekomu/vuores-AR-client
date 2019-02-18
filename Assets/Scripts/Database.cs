using System.Collections;
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
        net.Get("localhost:3000/api/v1/dbinfo/", SaveDBInf);
    }

    private void SaveDBInf(string res)
    {
        SetDatabaseInfo(JsonUtility.FromJson<DatabaseInfo>(res));
        Main.self.Next();
    }
    
    void ReadDatabase(NetworkInterface net)
    {
        for (int i = 0; i < DatabaseInfo.length; i++)
        {
            net.Get("localhost:3000/api/v1/id." + i.ToString(), SaveDatabaseEntry);
        }
    }
    
    void SaveDatabaseEntry(string res)
    {
        if (DatabaseEntries.Count < DatabaseInfo.length)
        {
            AddDBEntry(JsonUtility.FromJson<DatabaseEntry>(res));
        }
        else if (DatabaseEntries.Count == DatabaseInfo.length)
        {
            AddDBEntry(JsonUtility.FromJson<DatabaseEntry>(res));
            Main.self.Next();
        }
    }
}
