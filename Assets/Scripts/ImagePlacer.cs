using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePlacer : MonoBehaviour
{
    [SerializeField] private float width = 2f;
    [SerializeField] private float height = 2f;
    [Header("Test Settings")]
    [SerializeField] private int limit = 10;

    private Vector3[] positions;

    public void CalculatePositions()
    {
        Random.InitState(Mathf.RoundToInt((width*height+width+height)*1000));
        positions = new Vector3[limit];
        for (int i = 0; i < limit; i++)
        {
            Vector3 pos = Vector3.zero;
            while (pos.x >= -transform.localScale.x && pos.x <= transform.localScale.x && pos.z >= -transform.localScale.z && pos.z <= transform.localScale.z)
            {
                pos = GetVector();
            }
            positions[i] = pos;
        }
    }

    private bool checkOverlap (Vector3 i, Vector3 j)
    {
        float m = (i-j).magnitude;
        return m >= 2 ? false : true;
    }

    private Vector3 GetVector ()
    {
        return new Vector3(Random.Range(0, width) - width * 0.5f, 1, Random.Range(0, height) - height * 0.5f);
    }

    public void OnDrawGizmos ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, 0f, height));
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, 0f, transform.localScale.z));
        CalculatePositions();
        foreach (var pos in positions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(pos, new Vector3(1, 0, 1) * 2);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(pos, new Vector3(1, 0, 1)*2);
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(pos, 0.1f);
        }
    }
}
