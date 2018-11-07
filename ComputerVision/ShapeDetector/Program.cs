using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
﻿using System.Drawing;
using System.Windows;

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

            //Bitmap _timg = CC.BackgroundThreshold(_img, 255);
            _b = CC.Detect(_img, 150);
            Console.WriteLine(_b.Count);
            Blob.DrawBlobs(_img, _b);
            //imageHandler.ShowImage("Blobs", _img);
            _img.Save("blobs.bmp");
            Console.ReadKey();
            
        }
    }
}