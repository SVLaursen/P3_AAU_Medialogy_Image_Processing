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
            CommandConsole.Run();
        }

        public static void StartProgram(string bImagePath, string imagePath, string fileName)
        {
            string root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string rootPath = Path.GetFullPath(Path.Combine(root, "..")) + "/Export/";
            List<Blob> _b = new List<Blob>();

            Bitmap _img = ImageHandler.LoadImage(imagePath);
            Bitmap _bImg = ImageHandler.LoadImage(bImagePath);
            CCHandler CC = new CCHandler(_img);
            ColorProcessing colorProcess = new ColorProcessing();

            Bitmap _timg = CCHandler.BackgroundThreshold(_bImg, _img, CommandConsole.Threshold);
            
            //Console.WriteLine("\nDetecting Blobs...");
            //_b = CC.Compare(_timg);
            //Console.WriteLine(" Blobs Found: "+_b.Count);
            //Console.WriteLine(" Drawing Blobs...");
            //Blob.DrawBlobs(_timg, _b);
            
            Console.WriteLine("\nCleaning colors");
            _timg = colorProcess.CleanImage(_timg);
            
            Console.WriteLine("\nSaving file...");


            if (File.Exists(rootPath + fileName))
            {
                File.Delete(rootPath + "Mask.bmp");
            }
            Console.WriteLine(" Exporting Bitmap...");
            _mimg.Save(rootPath + "Mask.bmp");
            _timg.Save(rootPath + fileName);
            Console.WriteLine("\nBlob Detection Execution: Success!");

        }
    }
}