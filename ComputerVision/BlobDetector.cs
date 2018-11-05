using System;
using System.Numerics;
using System.Collections;
using Emgu.CV;
using Emgu.CV.Structure;

public class BlobDetector
{
    private ArrayList blobs = new ArrayList();
    private ArrayList colors;

    private int cThreshold;
    private int dThreshold;

    //Public Constructor. Requires an ArrayList of Vector3 Objects, containing color values in HSV Format, a color threshold and a distance threshold.
    public BlobDetector(ArrayList _colors, int _cThreshold, int _dThreshold)
    {
        colors = _colors;
        cThreshold = _cThreshold;
        dThreshold = _dThreshold;
    }


    //Detects all Blobs in an image that has a color within the specified threshold for the colors provided in the colors arraylist given on construct
    //then returns an ArrayList of Blob Objects.
    ArrayList detectBlobs(Mat _img)
    {
        Image<Hsv, Byte> imgC = _img.ToImage<Hsv, Byte>();
        //Iterate through pixels in image, looking for pixels with specific colours.
        foreach(Vector3 _c in colors)
        {
            for(int x = 0; x < imgC.Width; x++)
            {
                for(int y = 0; y < imgC.Height; y++)
                {
                    if (imgC.Data[x, y, 0] <= _c.X-cThreshold && imgC.Data[x, y, 0] >= _c.X + cThreshold)
                    {
                        //Pixel found. Look for nearby blobs.
                        if(blobs.Count <= 0)
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
        }
        return blobs;
    }

/*  void displayCamera()
    {
        VideoCapture cap = new VideoCapture(0);
        Mat frame = new Mat();
        while (true)
        {
            cap.Read(frame);

            CvInvoke.Imshow("Video Stream", frame);
            CvInvoke.WaitKey(1);
        }
    } */

/*  protected Mat fetchCamera(Int32 camera)
    {
        VideoCapture cap = new VideoCapture(camera);
        Mat frame = new Mat();
        cap.Read(frame);
        return frame;
    }*/
}