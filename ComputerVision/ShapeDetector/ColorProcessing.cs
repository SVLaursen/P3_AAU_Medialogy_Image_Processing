using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace ShapeDetector
{
    public class ColorProcessing
    {
        private List<Color> debugColors = new List<Color>(new []
        {
            Color.FromArgb(255, 0, 0), //RED
            Color.FromArgb(0, 255, 0), //GREEN
            Color.FromArgb(0, 0, 255), //BLUE
           // Color.FromArgb(0, 255, 255), //CYAN
            Color.FromArgb(255, 0, 255), //MAGENTA
            Color.FromArgb(255, 255, 0) //YELLOW
        });

        public Bitmap CleanImage(Bitmap _src, Bitmap _mask)
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();

            for (var y = 0; y < _src.Height; y++)
            {
                for (var x = 0; x < _src.Width; x++)
                {
                    if (_mask.GetPixel(x, y) == Color.FromArgb(255, 255, 255)) continue;

                    foreach (var t in debugColors)
                    {
                        if (ColorsAreClose(_src.GetPixel(x, y), t, CommandConsole.ColorThreshold)) _src.SetPixel(x, y, t);
                    }
                }
            }
            
            stop.Stop();
            Console.WriteLine(" Image Cleaned in: " + stop.ElapsedMilliseconds + " Milliseconds");
            
            return _src;

            bool ColorsAreClose(Color imgColor, Color listColor, int threshold)
            {
                var imgHue = GetHuePixel(imgColor);
                var listHue = GetHuePixel(listColor);

                return Math.Abs(imgHue - listHue) <= threshold;
            }
        }

        /*
         * BELOW IS THE CONVERSION ALGORITHMS
         * SOME OF THE CODE IS NO LONGER BEING USED
         */
        public Bitmap RGB2HSI(Bitmap img)
        {
            //NOT YET IMPLEMENTED
            return null;
        }

        public float[,] GetHue(Bitmap img)
        {
            var src = Split(img);
            var hue = new float[img.Width, img.Height];

            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    var red = src.channel_one[x, y];
                    var green = src.channel_two[x, y];
                    var blue = src.channel_three[x, y];

                    var pow = Math.Pow(red - green, 2);
                    var sqrt = Math.Sqrt((float) pow + (red - blue) * (green - blue));
                    var calc = (float) Math.Acos((0.5f * (red - green) + (red - blue)) / sqrt);

                    if (blue < green)
                    {
                        hue[x, y] = calc;
                    }
                    else
                    {
                        hue[x, y] = (float)(360 * Math.PI) / 180 - calc;
                    }
                }
            }

            return hue;
        }

        public float GetHuePixel(Color pixelColor)
        {
            int red = pixelColor.R,
                green = pixelColor.G,
                blue = pixelColor.B;
            
            var pow = Math.Pow(red - green, 2);
            var sqrt = Math.Sqrt((float) pow + (red - blue) * (green - blue));
            var calc = (float) Math.Acos((0.5f * (red - green) + (red - blue)) / sqrt);

            if (blue < green)
            {
                return calc;
            }

            return (float)(360 * Math.PI) / 180 - calc; //Error catcher
        }

        public float[,] GetSaturation(Bitmap img)
        {
            var src = Split(img);
            var saturation = new float[img.Width, img.Height];
            

            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    var red = src.channel_one[x, y];
                    var green = src.channel_two[x, y];
                    var blue = src.channel_three[x, y];
                    
                    var min =  Math.Min(Math.Min(red, green), blue);

                    saturation[x, y] = (float)(1 - 3 / (red + green + blue + 0.001) * min);
                }
            }

            return saturation;
        }

        public float[,] GetIntensity(Bitmap img)
        {
            var src = Split(img);
            var intensity = new float[img.Width, img.Height];

            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    var red = src.channel_one[x, y];
                    var green = src.channel_two[x, y];
                    var blue = src.channel_three[x, y];

                    intensity[x, y] = (red + green + blue) / 3;
                }
            }

            return intensity;
        }

        public ImageChannels Split(Bitmap img)
        {
            var red = new float[img.Width, img.Height];
            var green = new float[img.Width, img.Height];
            var blue = new float[img.Width, img.Height];

            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    var clr = img.GetPixel(x, y);
                    red[x, y] = clr.R;
                    green[x, y] = clr.G;
                    blue[x, y] = clr.B;
                }
            }

            var splitImg = new ImageChannels();
            splitImg.SetAllValues(red, green, blue);
            splitImg.height = img.Height;
            splitImg.width = img.Width;

            return splitImg;
        }

        public Bitmap Merge(ImageChannels splitImg)
        {
            var bmp = new Bitmap(splitImg.width, splitImg.height);

            for (var y = 0; y < splitImg.height; y++)
            {
                for (var x = 0; x < splitImg.width; x++)
                {
                    var red = (int)splitImg.channel_one[x, y];
                    var green = (int)splitImg.channel_two[x, y];
                    var blue = (int)splitImg.channel_three[x, y];

                    var clr = Color.FromArgb(red, green, blue);
                    bmp.SetPixel(x, y, clr);
                }
            }

            return bmp;
        }
    }

    public struct ImageChannels
    {
        public float[,] channel_one;
        public float[,] channel_two;
        public float[,] channel_three;

        public int height;
        public int width;

        public void SetAllValues(float[,] c_one, float[,] c_two, float[,] c_three)
        {
            channel_one = c_one;
            channel_two = c_two;
            channel_three = c_three;
        }

        public int[,] GetSize()
        {
            return new int[height, width];
        }

        public void SetSize(int height, int width)
        {
            channel_one = new float[height, width];
            channel_two = new float[height, width];
            channel_three = new float[height, width];
        }
    }
}
