using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Numerics;


namespace ShapeDetector
{
    class CCHandler
    {
        //Blob Detection Resources
        private static List<Blob> _b = new List<Blob>();
        private Blob blob;
        private static bool[,] isNotBlack;
        private static bool[,] isChecked;
        private static bool[,] isGrass;
        private static List<Point> queue = new List<Point>();

        //Public Constructor. Requires an image and a list of colors.
        public CCHandler(Bitmap _img)
        {
            isNotBlack = Binary(_img);
            isChecked = Binary(_img);
            isGrass = Binary(_img);
        }

        //Creates a boolean array of the supplied image. Used for the grassfire algorithm
        private static bool[,] Binary(Bitmap _img)
        {
            int h = _img.Height;
            int w = _img.Width;
            bool[,] b = new bool[w, h];
            return b;
        }
      
        //Compares two images and blacks out the pixels that are equal
        public Bitmap BackgroundThreshold(Bitmap bImg, Bitmap img, int t)
        {
            Bitmap _img = new Bitmap(img);
            Stopwatch stop = new Stopwatch();
            stop.Start();
            for (int x = 0; x < _img.Width; x++)
            {
                for (int y = 0; y < _img.Height; y++)
                {
                    if (!isChecked[x, y] && DeltaE(bImg.GetPixel(x, y), img.GetPixel(x, y)) > t)
                    {
                        isNotBlack[x, y] = true;
                        isChecked[x, y] = true;

                    }
                    else
                    {
                        _img.SetPixel(x, y, Color.Black);
                        isChecked[x, y] = true;
                        isNotBlack[x, y] = false;

                    }
                }
            }
            stop.Stop();
            Console.WriteLine(" Background removal time: " + stop.ElapsedMilliseconds + " Milliseconds");
            return _img;
        }

        //Grassfire to detect blobs based on binary array
        public void Grassfire(Point point, bool[,] arr, Blob blob)
        {
            int x = point.X;
            int y = point.Y;
            
                blob.Add(x, y);
                if (x + 1 < arr.GetLength(0) && !isGrass[x + 1, y] && isNotBlack[x + 1, y])
                {
                    queue.Add(new Point(x + 1, y));
                    blob.Add(x + 1, y);
                    isGrass[x + 1, y] = true;
                }
                if (y + 1 < arr.GetLength(1) && !isGrass[x, y + 1] && isNotBlack[x, y + 1])
                {
                    queue.Add(new Point(x, y + 1));
                    blob.Add(x, y + 1);
                    isGrass[x, y + 1] = true;
                }
                if (x - 1 >= 0 && !isGrass[x - 1, y] && isNotBlack[x - 1, y])
                {
                    queue.Add(new Point(x - 1, y));
                    blob.Add(x - 1, y);
                    isGrass[x - 1, y] = true;
                }
                if (y - 1 >= 0 && !isGrass[x, y - 1] && isNotBlack[x, y - 1])
                {
                    queue.Add(new Point(x, y - 1)); 
                    blob.Add(x, y - 1);
                    isGrass[x, y - 1] = true;
                }
        }

        //Finds blobs in the binary array isNotBlack
        public List<Blob> FindBlobs()
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
            for (int x = 0; x < isNotBlack.GetLength(0); x++)
            {
                for(int y = 0; y < isNotBlack.GetLength(1); y++)
                {
                    if(isNotBlack[x, y] && !isGrass[x, y])
                    {
                        blob = new Blob(x, y);
                        queue.Add(new Point(x, y));
                        while (queue.Count != 0)
                        {
                            Grassfire(queue[0], isNotBlack, blob);
                            queue.RemoveAt(0);
                        }
                        _b.Add(blob);
                    }
                }
            }
            stop.Stop(); 
            Console.WriteLine(" Finding Blobs time: " + stop.ElapsedMilliseconds + " Milliseconds");
            return _b;
        }

        //Not in use at the moment
        public Bitmap MaskInverse()
        {
            Bitmap _b = new Bitmap(isNotBlack.GetLength(0), isNotBlack.GetLength(1));
            for (int x = 0; x < isNotBlack.GetLength(0); x++)
            {
                for (int y = 0; y < isNotBlack.GetLength(1); y++)
                {
                    if(isNotBlack[x, y])
                    {
                        _b.SetPixel(x, y, Color.Black);
                    }
                }
              }
            return _b;
        }

        //Creates an output image with inverse masks of the found blobs
        public Bitmap MaskInverse(List<Blob> b)
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
            Bitmap _b = new Bitmap(isNotBlack.GetLength(0), isNotBlack.GetLength(1));
            foreach(Blob bl in b)
            {
                foreach(Vector2 p in bl.points)
                {
                    _b.SetPixel((int)p.X, (int)p.Y, Color.Black);
                }
            }
            stop.Stop();
            Console.WriteLine(" Masking time: " + stop.ElapsedMilliseconds + " Milliseconds");
            return _b;
        }


        //Uses the Euclidian Distance Formular to calculate distance between colors "Delta E"
        public static double DeltaE(Color c1, Color c2)
        {
            double deltaE = Math.Sqrt(Math.Pow(c1.R - c2.R, 2) + Math.Pow(c1.G - c2.G, 2) + Math.Pow(c1.B - c2.B, 2));

            return deltaE;

        }

        //Used to get the time it would take to deltaE an entire image to a specified color. Used for debugging
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
