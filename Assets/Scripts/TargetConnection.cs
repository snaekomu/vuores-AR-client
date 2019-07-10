using UnityEngine;
using Vuforia;

public class TargetConnection : MonoBehaviour
{
    [SerializeField] private ImageTargetBehaviour target;
    private string targetName;
    private GameObject imagePrefab;
    [SerializeField] private NetworkInterface net;

    public void Awake() {
        imagePrefab = Resources.Load("Prefabs/Image") as GameObject;

        if (!net) { net = gameObject.GetComponent<NetworkInterface>(); }
        if (!net) { net = gameObject.AddComponent<NetworkInterface>(); }
        if (!target) { target = gameObject.GetComponent<ImageTargetBehaviour>(); }
        
        targetName = target.name;
    }

    public void Start() {
        GetData();
    }

    private void GetData() {
        net.Get<ContentModel.Array>(NetworkInterface.Request("api/v1/target/contents", "name", targetName, "asObject", "true"), SpawnImages);
    }

    private void SpawnImages(ContentModel.Array res) {
        foreach (var element in res.elements)
        {
            (Instantiate(imagePrefab, transform) as GameObject).GetComponentInChildren<ImageConnection>().DownloadTexture(net, element.url);
        }
    }
}
