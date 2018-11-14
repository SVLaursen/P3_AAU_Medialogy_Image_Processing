using System.Collections;
using System.Collections.Generic;
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
    //public static string streamingAssetsPath = Application.dataPath + "/StreamingAssets";
    //public static string blobImageFileName = "testImg.bmp";

    public static Texture2D LoadImage(string filePath)
    { //NOTE: Sadly, the Unity Engine (not editor!) cannot load .bmp's, only .png's and .jpg's. If given anything else, it will return a standard 8x8 texture with a question mark.
      //Other than that, this function works, and will load images from a given filepath
        Debug.Log("Looking for image: " + filePath);
        Texture2D tex = null;
        byte[] fileData;

        if (System.IO.File.Exists(filePath))
        {
            Debug.Log("Found file");
            fileData = System.IO.File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2, TextureFormat.BGRA32, false);
            Debug.Log(tex.width);
            tex.LoadImage(fileData); //texture will be automatically resized.
            Debug.Log(tex.width);
        }

        return tex;
    }
}

[System.Serializable]
public struct ColorToPrefab
{
    public Color32 color;
    public GameObject prefab;
}
