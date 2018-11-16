﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Texture2D levelMap;
    public bool loadFromBytes;
    public string levelFileName;

    public List<ColorToPrefab> entities = new List<ColorToPrefab>();

    public void LoadLevel()
    {
        EmptyMap(); //Reset before starting the load process

        if (levelMap == null) Debug.LogError("No map loaded");

        if (loadFromBytes)
        {
            //Loads file as bytes from StreamingAssets folder (Maybe change to be the only possible way?)
            var filePath = Application.dataPath + "/StreamingAssets/" + levelFileName;
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            levelMap.LoadImage(bytes);
        }

        var allPixels = levelMap.GetPixels32();
        var width = levelMap.width;
        var height = levelMap.height;

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                SpawnAt(allPixels[x + (y * width)], x, y);
            }
        }
    }

    public void EmptyMap()
    {
        while (transform.childCount > 0)
        {
            var c = transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }
    }

    public void SetMap(Texture2D map)
    {
        levelMap = map;
    }

    private void SpawnAt(Color32 color, int x, int y)
    {
        if (color == Color.black) return;

        //Find the right color in our map
        foreach (var ctp in entities)
        {
            if (!color.Equals(ctp.color)) continue;

            //Spawn prefab at right location according to map 
            //Important! The last part of input in the below variable determines rotation!
            var obj = Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.SetParent(transform);

            return;
        }

        //Did not find matching prefab to input color
        Debug.LogError("No prefab found for " + color);
    }

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
                blobCount = br.ReadInt32();
                //Debug.Log(" Reading " + blobCount + " blobs...");
                readBlobs = new BlobStructure[blobCount];

                for (int i = 0; i < blobCount; i++)
                {
                    //Debug.Log("Reading nr. " + (i + 1));
                    Vector2[] corners = new Vector2[4];

                    int x = br.ReadInt32();
                    int y = br.ReadInt32();
                    //Debug.Log("First point: " + x + ", " + y);
                    corners[0] = new Vector2(x, y);

                    x = br.ReadInt32();
                    y = br.ReadInt32();
                    //Debug.Log("Second point: " + x + ", " + y);
                    corners[1] = new Vector2(x, y);

                    x = br.ReadInt32();
                    y = br.ReadInt32();
                    //Debug.Log("Third point: " + x + ", " + y);
                    corners[2] = new Vector2(x, y);

                    x = br.ReadInt32();
                    y = br.ReadInt32();
                    //Debug.Log("Fourth point: " + x + ", " + y);
                    corners[3] = new Vector2(x, y);

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

[System.Serializable]
public struct ColorToPrefab
{
    public Color32 color;
    public GameObject prefab;
}
