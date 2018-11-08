using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.IO;

namespace ShapeDetector
{   
    internal class Program
    {
        public static int thr = 50;

        public static void Main(string[] args)
        {
            CommandConsole.Run();
        }


        public static void StartProgram(List<Color> _c, int threshold, String imagePath, String fileName)
        {
            String root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            String rootPath = Path.GetFullPath(Path.Combine(root, "..")) + "/Export/";
            List<Blob> _b = new List<Blob>();

            Bitmap _img = ImageHandler.LoadImage(imagePath);
            CCHandler CC = new CCHandler(_c, _img);

            Console.WriteLine("Thresholding...");
            Bitmap _timg = CC.BackgroundThreshold(_img, thr);
            Console.WriteLine("Detecting Blobs...");
            _b = CC.Detect(_timg, thr);
            Console.WriteLine(_b.Count);
            Console.WriteLine("Drawing Blobs...");
            Blob.DrawBlobs(_timg, _b);


            if (File.Exists(rootPath + fileName + ".bmp"))
            {
                Console.WriteLine("Image Already Exists. Overwriting");
                File.Delete(rootPath + fileName + ".bmp");
            }
            Console.WriteLine("Exporting Bitmap");
            _timg.Save(rootPath + fileName +".bmp");
            Console.WriteLine("Blob Detection Execution: Success!");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadKey();

        }
    }
}