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
            //This block was made for testing the Grassfire class. Remove when we've tested it fully.
            
            Bitmap testImage = new Bitmap(@"D:\3SemesterImageProcessingProject\P3_AAU_Medialogy_Image_Processing\ComputerVision\ShapeDetector\SingleDot.png");
            Grassfire grassfire = new Grassfire();

            int[,] result = grassfire.RunGrassfire(testImage, 125, 10);

            for (var y = 0; y < testImage.Height; y++)
            {
                string message = "";
                for (var x = 0; x < testImage.Width; x++)
                {
                    message += result[x, y] + " ";
                }
                Console.WriteLine(message);
            }

            //NOTE: Main consoloe program is commented out for testing purposes, and replaced with a ReadKey
            Console.ReadKey();
            //CommandConsole.Run();
        }


        public static void StartProgram(List<Color> _c, int threshold, string imagePath, string fileName)
        {
            string root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string rootPath = Path.GetFullPath(Path.Combine(root, "..")) + "/Export/";
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