using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobLoaderTest : MonoBehaviour
{
    //NOTE: This entire class may be reworkable into a collider manager

    public static float scale = 1f;
    public BlobStructure[] blobStructures;
    public PolygonCollider2D[] colliders;

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
        blobStructures = LevelLoader.LoadBlobs(Application.streamingAssetsPath + "/binary");

        for (int i = 0; i < blobStructures.Length; i++)
        {
            CreateCollider(blobStructures[i], "block" + (i + 1));
        }

        Rescale(scale);
    }

    public GameObject CreateCollider(BlobStructure blob, string name)
    {
        GameObject g = new GameObject(name);
        g.AddComponent<PolygonCollider2D>();
        PolygonCollider2D poly = g.GetComponent<PolygonCollider2D>();
        poly.SetPath(0, blob.corners);
        g.transform.SetParent(transform);

        return g;
    }

    public void Rescale(float newScale)
    {
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
