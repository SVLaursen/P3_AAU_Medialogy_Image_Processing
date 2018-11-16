using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobLoaderTest : MonoBehaviour
{
    public BlobStructure blobStructure;

    // Use this for initialization
    void Start()
    {
        Test();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Test()
    {
        BlobStructure[] blobs = LevelLoader.LoadBlobs(Application.streamingAssetsPath + "/binary");
        blobStructure.corners = blobs[0].corners;
        blobStructure.color = blobs[0].color;
    }
}
