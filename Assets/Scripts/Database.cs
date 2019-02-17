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
}
