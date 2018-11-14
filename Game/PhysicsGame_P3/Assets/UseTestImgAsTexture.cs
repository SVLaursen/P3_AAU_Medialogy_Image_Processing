using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseTestImgAsTexture : MonoBehaviour
{
    //NOTE: This whole thing is only for testing the loadImage function in LevelLoader. It can safely be deleted later.
    public static string streamingAssetsPath;
    public static string blobImageFileName;

    // Use this for initialization
    void Start()
    {
        streamingAssetsPath = Application.dataPath + "/StreamingAssets";
        blobImageFileName = "testImg.png";


        Renderer myRenderer = GetComponent<Renderer>();

        Texture tex = LevelLoader.loadImage(streamingAssetsPath + "/" + blobImageFileName);
        Debug.Log(tex.dimension);
        

        myRenderer.material.mainTexture = tex;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
