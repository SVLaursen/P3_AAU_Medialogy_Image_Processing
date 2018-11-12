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


        public static void StartProgram(int threshold, string imagePath, string fileName)
        {
            string root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string rootPath = Path.GetFullPath(Path.Combine(root, "..")) + "/Export/";
            List<Blob> _b = new List<Blob>();

            Bitmap _img = ImageHandler.LoadImage(imagePath);
            CCHandler CC = new CCHandler(_img);

            Console.WriteLine("\nThresholding...");
            Console.WriteLine("\nDetecting Blobs...");
            _b = CC.Detect(_img, thr);
            Console.WriteLine(" Blobs Found: "+_b.Count);
            Console.WriteLine(" Drawing Blobs...");
            Blob.DrawBlobs(_img, _b);
            Console.WriteLine("\nSaving file...");


            if (File.Exists(rootPath + fileName))
            {
                Console.WriteLine(" File Already Exists. Overwriting");
                File.Delete(rootPath + fileName);
            }
            Console.WriteLine(" Exporting Bitmap...");
            _img.Save(rootPath + fileName);
            Console.WriteLine("\nBlob Detection Execution: Success!");
        }
        public static void StartProgram(string bImagePath, string imagePath, string fileName)
        {
            string root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string rootPath = Path.GetFullPath(Path.Combine(root, "..")) + "/Export/";
            List<Blob> _b = new List<Blob>();

            Bitmap _img = ImageHandler.LoadImage(imagePath);
            Bitmap _bImg = ImageHandler.LoadImage(bImagePath);
            CCHandler CC = new CCHandler(_img);

            Console.WriteLine("\nDetecting Blobs...");
            _b = CC.Compare(_bImg, _img);
            Console.WriteLine(" Blobs Found: "+_b.Count);
            Console.WriteLine(" Drawing Blobs...");
            Blob.DrawBlobs(_img, _b);
            Console.WriteLine("\nSaving file...");


            if (File.Exists(rootPath + fileName))
            {
                Console.WriteLine(" File Already Exists. Overwriting");
                File.Delete(rootPath + fileName);
            }
            Console.WriteLine(" Exporting Bitmap...");
            _img.Save(rootPath + fileName);
            Console.WriteLine("\nBlob Detection Execution: Success!");

        }
    }
}