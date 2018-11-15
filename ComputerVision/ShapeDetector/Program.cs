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

            Console.WriteLine("\nThresholding...");
            Bitmap _timg = CC.BackgroundSubtraction(_bImg, _img, CommandConsole.Threshold);

            Console.WriteLine("\nDetecting Blobs...");
            _b = CC.FindBlobs();
            Console.WriteLine(" Blobs Found: "+_b.Count);

            foreach(Blob b in _b)
            {
                b.DrawCorners(_timg, b.getCorners());
            }

            Console.WriteLine("\nCreating Mask...");
            Bitmap _mimg = CC.MaskInverse(_b);

            Console.WriteLine("\nCleaning colors");
            _timg = colorProcess.CleanImage(_timg);
            
            Console.WriteLine("\nSaving files...");
            if (File.Exists(rootPath + fileName))
            {
                Console.WriteLine(" File Already Exists. Overwriting");
                File.Delete(rootPath + fileName);
            }
            if (File.Exists(rootPath + "Mask.bmp"))
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