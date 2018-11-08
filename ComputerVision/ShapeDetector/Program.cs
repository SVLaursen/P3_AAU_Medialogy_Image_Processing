using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.IO;

namespace ShapeDetector
{   
    internal class Program
    {
        public static void Main(string[] args)
        {
<<<<<<< HEAD
            String root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            String rootPath = Path.GetFullPath(Path.Combine(root, ".."));
            List<Color> _c = new List<Color>();
            List<Blob> _b = new List<Blob>();
            _c.Add(Color.Red);
            _c.Add(Color.Blue);
            Color green = Color.FromArgb(000, 255, 000);
            _c.Add(green);
            int thr = 50;

            Bitmap _img = ImageHandler.LoadImage("test2.png");
            CCHandler CC = new CCHandler(_c, _img);

            Console.WriteLine("Thresholding...");
            Bitmap _timg = CC.BackgroundThreshold(_img, thr);
            Console.WriteLine("Detecting Blobs...");
            _b = CC.Detect(_timg, thr);
            Console.WriteLine(_b.Count);
            Console.WriteLine("Drawing Blobs...");
            Blob.DrawBlobs(_timg, _b);


            if(File.Exists(rootPath + "/Export/blob.bmp"))
            {
                Console.WriteLine("Image Already Exists. Replacing.");
                File.Delete(rootPath + "/Export/blob.bmp");
            }
            Console.WriteLine("Exporting Bitmap");
            _timg.Save(rootPath + "/Export/blob.bmp");
            Console.WriteLine("Done!");
            Console.ReadKey();
=======
            CommandConsole.Run();
        }
            

        public static void StartProgram()
        {
            //String root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //String rootPath = Path.GetFullPath(Path.Combine(root, ".."));
            //List<Color> _c = new List<Color>();
            //List<Blob> _b = new List<Blob>();
            //Color red = Color.FromArgb(255, 255, 0, 0);
            //_c.Add(Color.Red);
            //_c.Add(Color.Blue);
            //_c.Add(Color.Green);


            //ImageHandler imageHandler = new ImageHandler();
            //Bitmap _img = imageHandler.LoadImage("test.png");
            //CCHandler CC = new CCHandler(_c, _img);

            //Bitmap _timg = CC.BackgroundThreshold(_img, 255);
            //_b = CC.Detect(_img, 150);
            //Console.WriteLine(_b.Count);
            //Blob.DrawBlobs(_img, _b);
            //_img.Save(rootPath+ "/Export/blob.bmp");
>>>>>>> 0edd2e95e18ef034b6db4f81f38e7bda1b393473
            
        }
    }
}