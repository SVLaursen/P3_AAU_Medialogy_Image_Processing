using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobLoader : MonoBehaviour
{
    //NOTE: This entire class may be reworkable into a collider manager

    public static float scale = 1f;
    public BlobStructure[] blobStructures;
    public PolygonCollider2D[] colliders;
    
    public List<ColorToPrefab> collisionInfo;

    // Use this for initialization
    void Start()
    {
        Test();
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

    public void CreateCollider(BlobStructure blob, string name)
    {
        GameObject obj = new GameObject(name);
        obj.AddComponent<PolygonCollider2D>();
        PolygonCollider2D poly = obj.GetComponent<PolygonCollider2D>();
        poly.SetPath(0, blob.corners);
        obj.transform.SetParent(transform);

        var currentColor = blob.color;

        for (var j = 0; j < collisionInfo.Count; j++)
        {
            if (currentColor != collisionInfo[j].color) continue;

            if (collisionInfo[j].isMagnet)
            {
                obj.AddComponent<AreaEffector2D>();
                poly.usedByEffector = true;
                var effector = obj.GetComponent<AreaEffector2D>();

                effector.forceAngle = 25f;
                effector.forceMagnitude = 100f;
                effector.forceVariation = 5f;
            }
            else poly.sharedMaterial = collisionInfo[j].physics;

        }

        //return g;
    }

    public void Rescale(float newScale)
    {
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}

[System.Serializable]
public struct ColorToPrefab
{
    public Color32 color;
    public PhysicsMaterial2D physics;
    public bool isMagnet;

    public AreaEffector2D AdjustMagnet()
    {
        //In order to use this, in the assigning stage check if it is a magnet
        //If it is true then call the below code and then this function to set it up
        //GameObject.AddComponent<AreaEffector2D>();
        
        return new AreaEffector2D {forceAngle = 25f, forceMagnitude = 100f, forceVariation = 5f};
    }
}