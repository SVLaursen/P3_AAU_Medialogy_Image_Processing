using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace ShapeDetector
{
    internal class ImageHandler
    {
        //Returns a bitmap with the loaded image
        public static Bitmap LoadImage(string filepath)
        {
            var _bitmap = (Bitmap)Bitmap.FromFile(filepath);
            
            return _bitmap ?? null;
        }
    }
}
