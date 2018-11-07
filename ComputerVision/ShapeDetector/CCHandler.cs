using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeDetector
{
    class CCHandler
    {
        ImageHandler imgH = new ImageHandler();
        List<Color> _c;
        List<Blob> _b = new List<Blob>();
        Blob blob = null;
        Boolean[,] boo;
        Boolean[,] boo2;

        public CCHandler(List<Color> c, Bitmap _img)
        {
            _c = c;
            boo = Binary(_img);
            boo2 = boo;
        }

        public Boolean[,] Binary(Bitmap _img)
        {
            int h = _img.Height;
            int w = _img.Width;
            Boolean[,] b = new Boolean[h, w];
            return b;
        }
        public void Grassfire(Bitmap img, int x, int y, Blob currentBlob, Color c, int t)
        {
            if (!boo[x, y])
            {
                boo[x, y] = true;
                int k = 0;

                if (currentBlob == null)
                {
                    _b.Add(new Blob(x, y, _b.Count));
                    int i = _b.Count - 1;
                    blob = _b[i];
                    Console.WriteLine("New Blob!");
                    Console.WriteLine(c);
                }

                if (blob != null)
                {
                    if (ImageHandler.DeltaE(img.GetPixel(y, x), c) <= t)
                    {
                        blob.Add(x, y);
                        if (x + 1 < img.Height && k == 0)
                        {
                            Grassfire(img, x + 1, y, blob, c, t);
                            k++;
                        }
                        if (y + 1 < img.Width && k == 1)
                        {
                            Grassfire(img, x, y + 1, blob, c, t);
                            k++;
                        }
                        if (x - 1 >= 0 && k == 2)
                        {
                            Grassfire(img, x - 1, y, blob, c, t);
                            k++;
                        }
                        if (y - 1 >= img.Width && k == 3)
                        {
                            Grassfire(img, x, y + 1, blob, c, t);
                            k = 0;
                        }

                    }
                }
            }
        }
        public void ThrGrassfire(Bitmap img, int x, int y, Color c, int t)
        {
            if (!boo2[x, y])
            {
                boo2[x, y] = true;
                int k = 0;
                    if (ImageHandler.DeltaE(img.GetPixel(y, x), c) > t)
                    {
                        img.SetPixel(y, x, Color.Black);
                        if (x + 1 < img.Height && k == 0)
                        {
                            ThrGrassfire(img, x + 1, y, c, t);
                            k++;
                        }
                        if (y + 1 < img.Width && k == 1)
                        {
                            ThrGrassfire(img, x, y + 1, c, t);
                            k++;
                        }
                        if (x - 1 >= 0 && k == 2)
                        {
                            ThrGrassfire(img, x - 1, y, c, t);
                            k++;
                        }
                        if (y - 1 >= img.Width && k == 3)
                        {
                            ThrGrassfire(img, x, y + 1, c, t);
                            k = 0;
                        }

                    }
            }
        }

        public List<Blob> Detect(Bitmap img, int threshold)
        {
            for (int x = 0; x < img.Height; x++)
            {
                for (int y = 0; y < img.Width; y++)
                {
                    Color clr = img.GetPixel(y, x);
                    foreach(Color c in _c)
                    {
                        if(ImageHandler.DeltaE(clr, c) <= threshold)
                        {
                            Grassfire(img, x, y, null, c, threshold);
                        }
                    }
                }
            }
            return _b;
        }

        public Bitmap BackgroundThreshold(Bitmap img, int threshold)
        {
            for (int x = 0; x < img.Height; x++)
            {
                for (int y = 0; y < img.Width; y++)
                {
                    Color clr = img.GetPixel(y, x);
                    foreach (Color c in _c)
                    {
                        if (ImageHandler.DeltaE(clr, c) > threshold)
                        {
                            //ThrGrassfire(img, x, y, c, threshold);
                            img.SetPixel(y, x, Color.Black);
                        }
                    }
                }
            }
            return img;
        }
    }
}
