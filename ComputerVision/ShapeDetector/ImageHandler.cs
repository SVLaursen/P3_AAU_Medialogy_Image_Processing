﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ShapeDetector
{
    internal class ImageHandler
    {
        public static VideoCapture capture = new VideoCapture(1);
        //Returns a bitmap with the loaded image
        public static Bitmap LoadImage(string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                var ms = new MemoryStream(br.ReadBytes((int)fs.Length));
                var _bitmap = new Bitmap(ms);
                return _bitmap ?? null;
            }
        }

        public static void CaptureImage(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            Bitmap img = capture.QueryFrame().Bitmap;
            img.Save(file);
        }
    }    
}

