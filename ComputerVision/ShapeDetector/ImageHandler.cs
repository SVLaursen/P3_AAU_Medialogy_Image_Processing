using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeDetector
{
    class ImageHandler
    {
        public Bitmap loadImage(String filepath)
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

        public void showImage (String windowName, Bitmap image)
        {
            Form _form = new Form();
            _form.Text = windowName;
            _form.AutoSize = true;
            

            PictureBox pbox = new PictureBox();
            pbox.SizeMode = PictureBoxSizeMode.AutoSize;
            pbox.Image = image;

            _form.Controls.Add(pbox);
            _form.ShowDialog();
        }

    }
}
