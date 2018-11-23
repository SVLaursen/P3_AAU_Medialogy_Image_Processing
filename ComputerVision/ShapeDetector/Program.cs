using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.IO;

namespace ShapeDetector
{
    internal class Program
    {
        public static Bitmap backgroundImage;
        public static Bitmap shapeImage;
        public static List<Blob> bb = new List<Blob>();
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
            Console.WriteLine(" Blobs Found: " + _b.Count);
            foreach (Blob b in _b)
            {
                if (b.points.Count > 2000)
                {
                    bb.Add(b);
                }
            }

            Console.WriteLine("\nCreating Mask...");
            Bitmap _mimg = CC.MaskInverse(bb);

            Console.WriteLine("\nCleaning colors");
            _timg = colorProcess.CleanImage(_timg);

            foreach (Blob b in bb)
            {

                b.DrawCorners(_timg, b.getCorners());
            }
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

            Console.WriteLine("Exporting binary file...");
            //TODO: For now, the exporter spits out binaries into rootPath. Eventually, this should be into StreamingAssets.
            Exporter.Run(_b, rootPath, "binary", _timg);
            Console.WriteLine("Export complete.");
            _b.Clear();
            bb.Clear();
            _mimg.Dispose();
            _timg.Dispose();
            _img.Dispose();
            _bImg.Dispose();
        }

        //NOTE: There are critical AccessViolationExceptions happening when this is run.
        public static void StartProgram(Bitmap bgImg, Bitmap shapeImg, string fileName)
        {
            string root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string rootPath = Path.GetFullPath(Path.Combine(root, "..")) + "/Export/";
            List<Blob> _b = new List<Blob>();
            Bitmap _bgImg = bgImg;
            Bitmap _shapeImg = shapeImg;
            CCHandler CC = new CCHandler(shapeImg);
            ColorProcessing colorProcess = new ColorProcessing();

            Console.WriteLine("\nThresholding...");
            Bitmap _timg = CC.BackgroundSubtraction(_bgImg, _shapeImg, CommandConsole.Threshold);

            Console.WriteLine("\nDetecting Blobs...");
            _b = CC.FindBlobs();
            Console.WriteLine(" Blobs Found: " + _b.Count);

            //NOTE: Throws an AccessViolationException in Graphics.DrawLine for some reason. The reason is unclear, but it has something to do with the memory allocated for the image.
            //The problem seemingly only happens when we are working with Bitmaps stored in ram instead of a file. Thus, it is deactivated for now.
            //foreach (Blob b in _b)
            //{
            //    b.DrawCorners(_timg, b.getCorners());
            //}

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

            Console.WriteLine("Exporting binary file...");
            //TODO: For now, the exporter spits out binaries into rootPath. Eventually, this should be into StreamingAssets.
            Exporter.Run(_b, rootPath, "binary", _timg);
            Console.WriteLine("Export complete.");
        }
        
    }
}