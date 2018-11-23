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

                //For each blob, 11 integers are exported: 4 corner x y locations, and rgb color.
                foreach (Blob b in blobs)
                {
                    Point[] locations = b.getCorners();
                    foreach (Point p in locations)
                    {
                        bw.Write(p.X);
                        bw.Write((p.Y));
                    }

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