using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;


namespace ShapeDetector
{
    class CCHandler
    {
        //Blob Detection Resources
        List<Blob> _b = new List<Blob>();
        Blob blob;
        bool[,] boo;
        bool[,] boo2;

        //Public Constructor. Requires an image and a list of colors.
        public CCHandler(Bitmap _img)
        {
            boo = Binary(_img);
            boo2 = Binary(_img);
        }

        //Creates a boolean array of the supplied image. Used for the grassfire algorithm
        private bool[,] Binary(Bitmap _img)
        {
            int h = _img.Height;
            int w = _img.Width;
            Boolean[,] b = new Boolean[w, h];
            return b;
        }

        //Blob Detection executer.
        public List<Blob> Detect(Bitmap img, int threshold)
        {
            Console.WriteLine("Filtering Whites");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color clr = img.GetPixel(x, y);
                    if (DeltaE(clr, Color.FromArgb(255, 255, 255)) > threshold)
                    {
                        Grassfire(img, x, y, null, threshold);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine(" Time Elapsed: " + stopwatch.ElapsedMilliseconds + " Milliseconds");
            return _b;
        }

        public List<Blob> Compare(Bitmap bImg, Bitmap img)
        {
            Console.WriteLine("Comparing Images...");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    if (img.GetPixel(x, y) != bImg.GetPixel(x, y))
                    {
                        CGrassfire(bImg, img, x, y, null);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine(" Time Elapsed: " + stopwatch.ElapsedMilliseconds + " Milliseconds");
            return _b;
        }
        //Blob detection grassfire algorithm. The meat of the Blob detection. Finds colors within the threshold of our specified colors, and creates blobs accordingly.
        private void Grassfire(Bitmap img, int x, int y, Blob currentBlob, int t)
        {
            double de = DeltaE(img.GetPixel(x, y), Color.FromArgb(255, 255, 255));
            if (!boo[x, y])
            {
                boo[x, y] = true;
                int k = 0;

                if (currentBlob == null)
                {
                    _b.Add(new Blob(x, y, img.GetPixel(x, y)));
                    int i = _b.Count - 1;
                    blob = _b[i];
                    Console.WriteLine(" New Blob!");
                    Console.WriteLine(" "+img.GetPixel(x, y)+ "\n");
                }

                if (blob != null)
                {
                    if (de > t)
                    {
                        blob.Add(x, y);
                        if (x + 1 < img.Width && k == 0)
                        {
                            Grassfire(img, x + 1, y, blob, t);
                            k++;
                        }
                        if (y + 1 < img.Height && k == 1)
                        {
                            Grassfire(img, x, y + 1, blob, t);
                            k++;
                        }
                        if (x - 1 >= 0 && k == 2)
                        {
                            Grassfire(img, x - 1, y, blob, t);
                            k++;
                        }
                        if (y - 1 >= img.Height && k == 3)
                        {
                            Grassfire(img, x, y - 1, blob, t);
                            k = 0;
                        }
                    }
                }
            }

        }

        private void CGrassfire(Bitmap bImg, Bitmap img, int x, int y, Blob currentBlob)
        {
            if (!boo[x, y])
            {
                boo[x, y] = true;
                int k = 0;

                if (currentBlob == null)
                {
                    _b.Add(new Blob(x, y, img.GetPixel(x, y)));
                    int i = _b.Count - 1;
                    blob = _b[i];
                    Console.WriteLine(" New Blob!");
                    Console.WriteLine(" " + img.GetPixel(x, y) + "\n");
                }

                if (blob != null)
                {
                    if (img.GetPixel(x, y) != bImg.GetPixel(x, y))
                    {
                        blob.Add(x, y);
                        if (x + 1 < img.Width && k == 0)
                        {
                            CGrassfire(bImg, img, x + 1, y, blob);
                            k++;
                        }
                        if (y + 1 < img.Height && k == 1)
                        {
                            CGrassfire(bImg, img, x, y + 1, blob);
                            k++;
                        }
                        if (x - 1 >= 0 && k == 2)
                        {
                            CGrassfire(bImg, img, x - 1, y, blob);
                            k++;
                        }
                        if (y - 1 >= img.Height && k == 3)
                        {
                            CGrassfire(bImg, img, x, y - 1, blob);
                            k = 0;
                        }
                    }
                }
            }

        }
        //Removes any color that is not within the threshold of our specified colors.
        public Bitmap BackgroundThreshold(Bitmap img, Color c, int threshold)
        {
            Bitmap _img = new Bitmap(img);

            for (int x = 0; x < _img.Width; x++)
            {
                for (int y = 0; y < _img.Height; y++)
                {
                    double de = DeltaE(_img.GetPixel(x, y), c);
                    if (!boo2[x, y] && de > threshold)
                    {
                        boo2[x, y] = true;

                    }
                    if (!boo2[x, y] && de <= threshold)
                    {
                        img.SetPixel(x, y, Color.Black);
                        boo2[x, y] = true;

                    }
                }
            }
            return img;
        }

        //Uses the Euclidian Distance Formular to calculate distance between colors "Delta E"
        public static double DeltaE(Color c1, Color c2)
        {
            double deltaE = Math.Sqrt(Math.Pow(c1.R - c2.R, 2) + Math.Pow(c1.G - c2.G, 2) + Math.Pow(c1.B - c2.B, 2));

            return deltaE;

        }

        public static long DeltaEDebug(Bitmap img, Color c1)
        {
            var stop = new Stopwatch();
            stop.Start();
            for(int i = 0; i < img.Width; i++)
            {
                for(int j = 0; j < img.Height; j++)
                {
                    double k = DeltaE(c1, img.GetPixel(i, j));
                }
            }
            stop.Stop();
            return stop.ElapsedMilliseconds;
        }
    }
}
