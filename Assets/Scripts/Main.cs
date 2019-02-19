using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private NetworkInterface networkInterface;
    [SerializeField] private Database database;
    
    //private float loadProgress = 0f;
    
    public static Main self;
    private delegate void StepDelegate();
    private int step = 0;
    private StepDelegate[] ActionsList;

    public void Awake()
    {
        self = this;
        ActionsList = new StepDelegate[]{
            GetDBInf,
            ReadDatabase,
            GetTextures
        };
    }

    //Start the loading process
    public void Start()
    {
        Debug.Log("Starting download process");
        Next(); 
    }
    
    //Call the next step in the loading process
    public void Next()
    {
        ActionsList[step++]();
    }

    //Get the database information
    void GetDBInf()
    {
        database.GetDBInf(networkInterface);
    }

    //Get whole database
    void ReadDatabase()
    {
        database.ReadDatabase(networkInterface);
    }

    //Download textures from urls and assign them to images
    void GetTextures()
    {
        UpdatableImage[] uis = FindObjectsOfType<UpdatableImage>();
        for (int i = 0; i < uis.Length; i++)
        {
            uis[i].DownloadTexture(networkInterface, database);
        }
    }
}
