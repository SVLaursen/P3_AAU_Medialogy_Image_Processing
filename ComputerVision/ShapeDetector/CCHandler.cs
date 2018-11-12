using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShapeDetector
{
    class CCHandler
    {
        //Blob Detection Resources
        List<Color> _c;
        List<Blob> _b = new List<Blob>();
        Blob blob;
        bool[,] boo;
        bool[,] boo2;

        //Public Constructor. Requires an image and a list of colors.
        public CCHandler(List<Color> c, Bitmap _img)
        {
            _c = c;
            boo = Binary(_img);
            boo2 = Binary(_img);
        }

        //Creates a boolean array of the supplied image. Used for the grassfire algorithm
        private bool[,] Binary(Bitmap _img)
        {
            int h = _img.Height;
            int w = _img.Width;
            Boolean[,] b = new Boolean[h, w];
            return b;
        }

        //Blob detection grassfire algorithm. The meat of the Blob detection. Finds colors within the threshold of our specified colors, and creates blobs accordingly.
        private void Grassfire(Bitmap img, int x, int y, Blob currentBlob, Color c, int t)
        {
            double de = DeltaE(img.GetPixel(y, x), c);
            if (!boo[x, y])
            {
                boo[x, y] = true;
                int k = 0;

                if (currentBlob == null)
                {
                    _b.Add(new Blob(x, y, _b.Count));
                    int i = _b.Count - 1;
                    blob = _b[i];
                    Console.WriteLine("New Blob!");
                    Console.WriteLine(c);
                }
                if (blob != null)
                {
                    if (de <= t)
                    {
                        blob.Add(x, y);
                        if (x + 1 < img.Height && k == 0)
                        {
                            Grassfire(img, x + 1, y, blob, c, t);
                            k++;
                        }
                        if (y + 1 < img.Width && k == 1)
                        {
                            Grassfire(img, x, y + 1, blob, c, t);
                            k++;
                        }
                        if (x - 1 >= 0 && k == 2)
                        {
                            Grassfire(img, x - 1, y, blob, c, t);
                            k++;
                        }
                        if (y - 1 >= img.Width && k == 3)
                        {
                            Grassfire(img, x, y + 1, blob, c, t);
                            k = 0;
                        }

                    }
                }
            }
        }

        //Blob Detection executer.
        public List<Blob> Detect(Bitmap img, int threshold)
        {
            for (int x = 0; x < img.Height; x++)
            {
                for (int y = 0; y < img.Width; y++)
                {
                    Color clr = img.GetPixel(y, x);
                    foreach(Color c in _c)
                    {
                        if(DeltaE(clr, c) <= threshold)
                        {
                            Grassfire(img, x, y, null, c, threshold);
                        }
                    }
                }
            }
            return _b;
        }

        //Removes any color that is not within the threshold of our specified colors.
        public Bitmap BackgroundThreshold(Bitmap img, int threshold)
        {
            Bitmap _img = new Bitmap(img);
           
            for (int y = 0; y < _img.Width; y++)
            {
                for (int x = 0; x < _img.Height; x++)
                {
                    int k = _c.Count;
                    foreach (Color c in _c)
                    {
                        double de = DeltaE(_img.GetPixel(y, x), c);
                        if (!boo2[x, y] && de <= threshold)
                        {
                            boo2[x, y] = true;
                            break;

                        }
                        if (!boo2[x, y] && de > threshold && k > 1)
                        {
                            k--;
                            continue;
                        }
                        if (!boo2[x, y] && de > threshold && k <= 1){
                            img.SetPixel(y, x, Color.Black);
                            boo2[x, y] = true;
                            
                        }
                    }
                }
            }
            return img;
        }

        //Uses the Euclidian Distance Formular to calculate distance between colors "Delta E"
        private double DeltaE(Color c1, Color c2)
        {
            double deltaE = Math.Sqrt(Math.Pow(c1.R - c2.R, 2) + Math.Pow(c1.G - c2.G, 2) + Math.Pow(c1.B - c2.B, 2));

            return deltaE;

        }
    }
}
