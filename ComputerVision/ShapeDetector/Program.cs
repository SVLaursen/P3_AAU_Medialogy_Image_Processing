using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows;
using System.IO;

namespace ShapeDetector
{   
    internal class Program
    {   
        public static void Main(string[] args)
        {
            String root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            String rootPath = Path.GetFullPath(Path.Combine(root, ".."));
            List<Color> _c = new List<Color>();
            List<Blob> _b = new List<Blob>();
            Color red = Color.FromArgb(255, 255, 0, 0);
            _c.Add(Color.Red);
            _c.Add(Color.Blue);
            _c.Add(Color.Green);


            ImageHandler imageHandler = new ImageHandler();
            Bitmap _img = imageHandler.LoadImage("test2.png");
            CCHandler CC = new CCHandler(_c, _img);

            //Bitmap _timg = CC.BackgroundThreshold(_img, 255);
            _b = CC.Detect(_img, 150);
            Console.WriteLine(_b.Count);
            Blob.DrawBlobs(_img, _b);
            _img.Save(rootPath+ "/Export/blob.bmp");
            Console.ReadKey();
            
        }
    }
}