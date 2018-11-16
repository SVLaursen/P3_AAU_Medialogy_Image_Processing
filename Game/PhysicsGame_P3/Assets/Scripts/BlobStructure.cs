using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlobStructure  {
    public Vector2[] corners;
    public Color color;
	
    public BlobStructure(Vector2[] corners, Color color)
    {
        this.corners = corners;
        this.color = color;
    }

    public BlobStructure()
    {
    }
}
