
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShapeDetector
{   
    internal class Program
    {   
        public static void Main(string[] args)
        {
            List<Color> _c = new List<Color>();
            List<Blob> _b = new List<Blob>();
            Color red = Color.FromArgb(255, 255, 0, 0);
            _c.Add(Color.Red);
            _c.Add(Color.Blue);
            _c.Add(Color.Green);


            ImageHandler imageHandler = new ImageHandler();
            Bitmap _img = imageHandler.LoadImage("test.png");
            CCHandler CC = new CCHandler(_c, _img);

            //Bitmap _timg = imageHandler.Threshold(_img, _c, 10);
            _b = CC.Detect(_img, 150);
            Console.WriteLine(_b.Count);
            Blob.DrawBlobs(_img, _b);
            Console.ReadKey();

        }
    }
}