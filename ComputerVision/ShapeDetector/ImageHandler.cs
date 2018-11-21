﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using DirectShowLib;

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

        public static Bitmap CaptureImage()
        {
            VideoCapture capture = new VideoCapture(); //create a camera capture
            
            return capture.QueryFrame().Bitmap; //take a picture
        }
        
    }    
}

