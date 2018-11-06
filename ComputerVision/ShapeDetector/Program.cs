using System.Drawing;
using System.Windows;

namespace ShapeDetector
{   
    internal class Program
    {   
        public static void Main(string[] args)
        {
            ImageHandler imageHandler = new ImageHandler();
            Bitmap _img = imageHandler.loadImage("test.jpg");
            imageHandler.showImage("test", _img);
        }
    }
}