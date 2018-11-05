/*using System;
using System.Numerics;
using System.Collections;
class Blob
{
    //Class Variables
    public Vector2 maxP;
    public Vector2 minP;
    private ArrayList points = new ArrayList();

    //Public constructor
    public Blob(int _x, int _y)
    {
        maxP = new Vector2(_x, _y);
        minP = new Vector2(_x, _y);
        points.Add(new Vector2(_x, _y));
    }

    //Determines wether the blob is withing a certain distance of a specified point
    public Boolean isNear(int _x, int _y, int _threshold)
    {
        Vector2 point = new Vector2(_x, _y);
        double d = double.MaxValue;

        foreach(Vector2 v in points)
        {
            float _d = Vector2.DistanceSquared(point, v);
            if(_d < d)
            {
                d = _d;
            }
        }

        if(d < Math.Pow(_threshold, 2)){ return true; }
        else { return false; }
    }

    //Returns the blobs distance to a specified point
    public float DistanceTo(int _x, int _y)
    {
        Vector2 point = new Vector2(_x, _y);
        double d = double.MaxValue;

        foreach (Vector2 v in points)
        {
            float _d = Vector2.DistanceSquared(point, v);
            if (_d < d)
            {
                d = _d;
            }
        }
        return (float) d;
    }
    //Adds a new pixel to the blob
    public void Add(int newx, int newy)
    {
        points.Add(new Vector2(newx, newy));
        minP.X = Math.Min(maxP.X, newx);
        minP.Y = Math.Min(maxP.Y, newy);
        maxP.X = Math.Max(maxP.X, newx);
        maxP.Y = Math.Max(maxP.Y, newy);
    }

    //Returns an Array of the Blob Corners in a clockwise rotation
    public Vector2[] getCorners()
    {
        Vector2[] corners = new Vector2[4];
        corners[0] = minP;
        corners[1] = new Vector2(minP.X, maxP.Y);
        corners[2] = maxP;
        corners[3] = new Vector2(maxP.X, minP.Y);

        return corners;
    }

}*/
