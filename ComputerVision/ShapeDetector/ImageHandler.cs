using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace ShapeDetector
{
    class ImageHandler
    {
        //Returns a bitmap with the loaded image
        public static Bitmap LoadImage(String filepath)
        {
            Bitmap _bitmap = (Bitmap)Bitmap.FromFile(@filepath);
            if(_bitmap != null)
            {
                return _bitmap;
            }
            else
            {
                Console.WriteLine("Image at " + filepath + "is null");
                return null;
            }
        }


    }
}
