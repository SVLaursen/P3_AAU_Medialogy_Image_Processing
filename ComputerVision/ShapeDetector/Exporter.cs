using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace ShapeDetector
{
    //Exports blob information as a binary file.
    public class Exporter
    {

        public static void Run(List<Blob> blobs, string target, string filename, Bitmap colorImage)
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream(target + "\\" + filename, FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot write file.");
                return;
            }

            try
            {
                //First, write the scale factor 
                //TODO: not quite implemented, replace placeholder value when done
                float scaleFactor = 0.01f;
                bw.Write(scaleFactor);

                //Then, the number of blobs are stored.
                bw.Write(blobs.Count);

                //For reference, half the width and height of the image is found
                int halfWidth = colorImage.Width / 2;
                int halfHeight = colorImage.Height / 2;

                //For each blob, 11 integers are exported: 4 corner x y locations, and rgb color.
                foreach (Blob b in blobs)
                {
                    Point[] locations = b.getCorners();
                    foreach (Point p in locations)
                    {
                        //Corner coordinates, adjusted by half of the image, are written.
                        bw.Write(p.X - halfWidth);
                        bw.Write((p.Y - halfHeight));
                    }

                    //Fetch color from center of blob
                    Point centerPoint = Blob.findCenterByCorners(locations);
                    Color sample = Blob.sampleColor(centerPoint, colorImage);
                    bw.Write(sample.R);
                    bw.Write(sample.G);
                    bw.Write(sample.B);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot write to file.");
                return;
            }
            bw.Close();

        }
    }
}