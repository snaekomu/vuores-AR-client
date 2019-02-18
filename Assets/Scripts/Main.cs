using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private NetworkInterface networkInterface;
    [SerializeField] private Database database;
    
    private float loadProgress = 0f;
    private static Main self;
    private delegate void Delegate();

    private int step = 0;

    private Delegate[] ActionsList =
    {
        GetDBInf,
        ReadDatabase,
        GetTextures
    };

    private void Awake()
    {
        self = this;
    }

    public void Start()
    {
        Next();
    }
    
    void Next()
    {
        ActionsList[step++];
    }

    void GetDBInf()
    {
        database.GetDBInf(networkInterface);
    }

    void ReadDatabase()
    {
        database.ReadDatabase(networkInterface);
    }

    void GetTextures()
    {
        for (int i = 0; i < database.DatabaseEntries.Count; i++)
        {
            if (i < database.DatabaseEntries.Count)
            {
                database.DatabaseEntries[i].LoadTexture(networkInterface);
            }
            else if (i < database.DatabaseEntries.Count)
            {
                database.DatabaseEntries[i].LoadTexture(networkInterface);
            }
        }
    }

    void ImageLoadProgress()
    {
        loadProgress += loadProgress + 1 / database.DatabaseEntries.Count;
        if (loadProgress >= 1)
        {
            ActivateImages();   
        }
    }

    void ActivateImages()
    {
        
    }
}
