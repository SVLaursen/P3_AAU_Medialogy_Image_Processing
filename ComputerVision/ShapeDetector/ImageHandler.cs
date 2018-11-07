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
    class ImageHandler
    {
        //Returns a bitmap with the loaded image
        public Bitmap LoadImage(String filepath)
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

        //Displays Image in a new Window
        public void ShowImage (String windowName, Bitmap image)
        {
            if(windowName == null)
            {
                Console.WriteLine("Window Name is empty. Using Default name");
                windowName = "Window";
            }
            if(image == null)
            {
                Console.WriteLine("Image is Null. Terminating");
                return;
            }
            Form _form = new Form();
            _form.Text = windowName;
            _form.AutoSize = true;
            

            PictureBox pbox = new PictureBox();
            pbox.SizeMode = PictureBoxSizeMode.AutoSize;
            pbox.Image = image;

            _form.Controls.Add(pbox);
            _form.ShowDialog();
        }

        //Turns all pixels with a distance, to the colors in the supplied list of colors, higher than the supplied threshold, to black.
        public Bitmap Threshold(Bitmap img, List<Color> c, int t)
        {
            Bitmap _img = img;
            Color black = Color.FromArgb(255, 0, 0, 0);
            for(int y = 0; y < _img.Height; y++)
            {
                for(int x = 0; x < _img.Width; x++)
                {
                    foreach(Color _c in c ){
                        Color _cc = 
                            _img.GetPixel(x, y);
                        if(DeltaE(_c, _cc) > t)
                        {
                            _img.SetPixel(x, y, black);
                        }

                    }
                }
            }
            return _img;
        }

        //Uses the Euclidian Distance Formular to calculate distance between colors "Delta E"
        public static double DeltaE(Color c1, Color c2)
        {
            double deltaE = Math.Sqrt(Math.Pow(c1.R - c2.R, 2) + Math.Pow(c1.G - c2.G, 2) + Math.Pow(c1.B - c2.B, 2));

            return deltaE;  

        }
    }
}
