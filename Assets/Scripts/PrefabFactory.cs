using UnityEngine;

[CreateAssetMenu]
public class PrefabFactory : ScriptableObject
{
    [SerializeField] private Transform[] prefabs;

    
    //Return a prefab from a given ID
    public Transform Get(int prefabIndex)
    {
        Transform instance;
        instance = Instantiate(prefabs[prefabIndex]);
        instance.gameObject.SetActive(false);
        return instance;
    }
}
