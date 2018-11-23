using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    //NOTE: to get the location of the StreamingAssets folder, Application.datapath has to be used.
    //public static string streamingAssetsPath = Application.streamingAssetsPath;
    //public static string blobImageFileName = "binary";

    public static BlobStructure[] LoadBlobs(string filePath)
    {
        Debug.Log("Looking for binary file: " + filePath);
        if (File.Exists(filePath))
        {

            BinaryReader br;
            int blobCount;
            BlobStructure[] readBlobs;

            try
            {
                br = new BinaryReader(new FileStream(filePath, FileMode.Open));
            }
            catch (IOException e)
            {
                Debug.LogError(e.Message + "\n Cannot read from file.");
                return null;
            }

            try
            {
                //NOTE: scale info is sent to BlobLoaderTest. May need change.
                BlobLoader.scale = br.ReadSingle();
                blobCount = br.ReadInt32();
                //Debug.Log(" Reading " + blobCount + " blobs...");
                readBlobs = new BlobStructure[blobCount];

                for (int i = 0; i < blobCount; i++)
                {
                    //Debug.Log("Reading nr. " + (i + 1));
                    Vector2[] corners = new Vector2[4];

                    //NOTE: In Unity, positive y goes upwards, while in screen space, it is reverse.
                    int x = br.ReadInt32();
                    int y = br.ReadInt32();
                    //Debug.Log("First point: " + x + ", " + y);
                    corners[0] = new Vector2(x, -y);

                    x = br.ReadInt32();
                    y = br.ReadInt32();
                    //Debug.Log("Second point: " + x + ", " + y);
                    corners[1] = new Vector2(x, -y);

                    x = br.ReadInt32();
                    y = br.ReadInt32();
                    //Debug.Log("Third point: " + x + ", " + y);
                    corners[2] = new Vector2(x, -y);

                    x = br.ReadInt32();
                    y = br.ReadInt32();
                    //Debug.Log("Fourth point: " + x + ", " + y);
                    corners[3] = new Vector2(x, -y);

                    //NOTE: color values are uint8. They are read as bytes.
                    byte r = br.ReadByte();
                    byte g = br.ReadByte();
                    byte b = br.ReadByte();

                    //Debug.Log("Color: " + r + ", " + g + ", " + b);
                    Color color = new Color32(r, g, b, 255);

                    readBlobs[i] = new BlobStructure(corners, color);
                }
            }
            catch (IOException e)
            {
                Debug.LogError(e.Message + "\n Cannot read from file.");
                return null;
            }

            br.Close();
            return readBlobs;
        }
        Debug.LogError("No file found!");
        return null;
    }

}


