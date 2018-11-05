using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Blob
{
    public class BlobDetector
    {
        private List<Blob> blobs = new List<Blob>();
        private List<Vector3> colors;

        private int cThreshold;
        private int dThreshold;

        //Public Constructor. Requires an ArrayList of Vector3 Objects, containing color values in HSV Format, a color threshold and a distance threshold.
        public BlobDetector(List<Vector3> _colors, int _cThreshold, int _dThreshold)
        {
            colors = _colors;
            cThreshold = _cThreshold;
            dThreshold = _dThreshold;
        }


        //Detects all Blobs in an image that has a color within the specified threshold for the colors provided in the colors arraylist given on construct
        //then returns an List of Blob Objects.
        List<Blob> detectBlobs(Mat _img)
        {
            Image<Hsv, Byte> imgC = _img.ToImage<Hsv, Byte>();
            //Iterate through pixels in image, looking for pixels with specific colours.
            Parallel.ForEach(colors, _c =>
            {
                for (int x = 0; x < imgC.Width; x++)
                {
                    for (int y = 0; y < imgC.Height; y++)
                    {
                        if (imgC.Data[x, y, 0] <= _c.X - cThreshold && imgC.Data[x, y, 0] >= _c.X + cThreshold)
                        {
                            //Pixel found. Look for nearby blobs.
                            if (blobs.Count <= 0)
                            {
                                //ArrayList of Blobs is empty, so no blobs has been found. Create new blob at the specified pixel and add it to the ArrayList.
                                Blob blob = new Blob(x, y);
                                blobs.Add(blob);
                            }
                            else
                            {
                                //ArrayList is not empty. Look for blobs.
                                Boolean found = false;
                                Blob minDistBlob = new Blob(0, 0);
                                foreach (Blob _b in blobs)
                                {
                                    float minDist = float.MaxValue;
                                    if (_b.isNear(x, y, dThreshold))
                                    {
                                        //Blob within distance threshold has been found. Determine if it is the closest blob.
                                        if (_b.DistanceTo(x, y) < minDist)
                                        {
                                            minDist = _b.DistanceTo(x, y);
                                            minDistBlob = _b;
                                        }
                                        found = true;
                                    }
                                }
                                if (found)
                                {
                                    //Add new pixel to the closest blob.
                                    minDistBlob.Add(x, y);
                                }
                                else
                                {
                                    //No blob has been found within the distance threshold, create new blob at pixel location.
                                    Blob blob = new Blob(x, y);
                                    blobs.Add(blob);
                                }
                            }
                        }
                    }
                }
            });
            return blobs;
        }
    }

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

            foreach (Vector2 v in points)
            {
                float _d = Vector2.DistanceSquared(point, v);
                if (_d < d)
                {
                    d = _d;
                }
            }

            if (d < Math.Pow(_threshold, 2)) { return true; }
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
            return (float)d;
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

    }
    public class Camera {
        //Displays the camera in a window
        public void display(Int32 camera)
        {
            VideoCapture cap = new VideoCapture(camera);
            Mat frame = new Mat();
            while (true)
            {
                cap.Read(frame);

                CvInvoke.Imshow("Video Stream", frame);
                CvInvoke.WaitKey(1);
            }
        }

        //Returns an image from the camera
        public Mat fetch(Int32 camera)
        {
            VideoCapture cap = new VideoCapture(camera);
            Mat frame = new Mat();
            cap.Read(frame);
            return frame;
        }
     }
}