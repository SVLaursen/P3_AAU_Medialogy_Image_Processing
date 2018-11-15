using System;
using System.IO;


namespace ShapeDetector
{

    public class Exporter
    {
        public void Run(Blob[] blobs, string target, string filename)
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream(target + "\\" + filename, FileMode.Create));
            }
            catch (IOException e){
                Console.WriteLine(e.Message + "\n Cannot write file.");
                return;
            }

            try {
                foreach (Blob b in blobs)
                {

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