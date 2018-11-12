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

            Bitmap testImage = ImageHandler.LoadImage("image.png");
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
            

            CommandConsole.Run();
        }


        public static void StartProgram(Color _c, int threshold, string imagePath, string fileName)
        {
            string root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string rootPath = Path.GetFullPath(Path.Combine(root, "..")) + "/Export/";
            List<Blob> _b = new List<Blob>();

            Bitmap _img = ImageHandler.LoadImage(imagePath);
            CCHandler CC = new CCHandler(_img);

            Console.WriteLine("\nThresholding...");
            Bitmap _timg = CC.BackgroundThreshold(_img, _c, thr);
            Console.WriteLine("\nDetecting Blobs...");
            _b = CC.Detect(_timg, thr);
            Console.WriteLine(" Blobs Found: "+_b.Count);
            Console.WriteLine(" Drawing Blobs...");
            Blob.DrawBlobs(_timg, _b);
            Console.WriteLine("\nSaving file...");


            if (File.Exists(rootPath + fileName))
            {
                Console.WriteLine(" File Already Exists. Overwriting...");
                File.Delete(rootPath + fileName);
            }
            Console.WriteLine(" Exporting Bitmap...");
            _timg.Save(rootPath + fileName);
            Console.WriteLine("\nBlob Detection Execution: Success!");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadKey();
            Console.Clear();

        }
    }
}