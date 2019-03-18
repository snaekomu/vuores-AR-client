

using System.IO;
using UnityEngine;

public class FileReadWrite : MonoBehaviour
{
      private string dir;

      private void Awake()
      {
            dir = Path.Combine(Application.persistentDataPath, "localdb");
      }

      private void Write()
      {
            using (BinaryWriter writer = new BinaryWriter(File.Open(dir, FileMode.Create)))
            {
                  writer.wri
            }
      }
}