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
            Color.FromArgb(255, 0, 255), //RED
            Color.FromArgb(0, 255, 0), //GREEN
            Color.FromArgb(0, 0, 255), //BLUE
            Color.FromArgb(0, 255, 255), //CYAN
            Color.FromArgb(255, 0, 255), //MAGENTA
            Color.FromArgb(255, 255, 0) //YELLOW
        });

        public Bitmap CleanImage(Bitmap _src)
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();

            PixelHandler src = new PixelHandler(_src);
            int bpp = Bitmap.GetPixelFormatSize(_src.PixelFormat) / 8;

            for (var y = 0; y < _src.Height; y++)
            {
                for (var x = 0; x < src.Width(); x += bpp)
                {
                    int _x = x / bpp;
                    if (src.GetPixel(_x, y) == Color.FromArgb(0, 0, 0)) continue;

                    foreach (var t in debugColors)
                    {
                        if (ColorsAreClose(src.GetPixel(_x, y), t, CommandConsole.ColorThreshold)) src.SetPixel(_x, y, t);
                    }
                }
            }
            stop.Stop();
            Console.WriteLine(" Image Cleaned in: " + stop.ElapsedMilliseconds + " Milliseconds");
            return src.Return();

            //Below is the new Hue comparing that Joakim wanted us to use instead, still needs a bit of tinkering but it is better
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
        public Bitmap RGB2HSV(Bitmap img)
        {
            Console.WriteLine("This function has not been implemented");
            //TODO: Make full image conversion
            return null;
        }

        public float[,] GetHue(Bitmap img)
        {
            var value = GetValue(img);
            var src = Split(img);
            var hue = new float[img.Width, img.Height];

            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    var red = src.channel_one[x, y];
                    var green = src.channel_two[x, y];
                    var blue = src.channel_three[x, y];

                    const float degrees = 60 * (float)Math.PI / 180;
                    var min = Math.Min(Math.Min(red, green), blue);

                    if (value[x, y] == red && green >= blue)
                        hue[x, y] = (green - blue) / (value[x, y] - min) * degrees;

                    else if (green == value[x, y])
                        hue[x, y] = ((blue - red) / (value[x, y] - min) + 2) * degrees;

                    else if (blue == value[x, y])
                        hue[x, y] = ((red - green) / (value[x, y] - min) + 4) * degrees;

                    else if (value[x, y] == red && green < blue)
                        hue[x, y] = ((red - blue) / (value[x, y] - min) + 5) * degrees;
                }
            }

            return hue;
        }

        public float GetHuePixel(Color pixelColor)
        {
            int red = pixelColor.R,
                green = pixelColor.G,
                blue = pixelColor.B;

            const float degrees = 60 * (float) Math.PI / 180;
            var  min = Math.Min(Math.Min(red, green), blue);
            var value = GetValuePixel(pixelColor);

            if (value == red && green >= blue)
                return (green - blue) / (value - min) * degrees;
            if (green == value)
                return ((blue - red) / (value - min) + 2) * degrees;
            if (blue == value)
                return ((red - green) / (value - min) + 4) * degrees;
            if (value == red && green < blue)
                return ((red - blue) / (value - min) + 5) * degrees;

            return 0; //Error catcher
        }

        public float GetValuePixel(Color pixelColor)
        {
            int red = pixelColor.R,
                green = pixelColor.G,
                blue = pixelColor.B;

            return Math.Max(Math.Max(red, green), blue);
        }


        public float[,] GetValue(Bitmap img)
        {
            var src = Split(img);
            var value = new float[img.Width, img.Height];

            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    value[x, y] = Math.Max(Math.Max(src.channel_one[x, y], src.channel_two[x, y]),
                        src.channel_three[x, y]);
                }
            }

            return value;
        }

        public float[,] GetSaturation(Bitmap img)
        {
            var src = Split(img);
            var value = GetValue(img);
            var saturation = new float[img.Width, img.Height];

            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    var red = src.channel_one[x, y];
                    var green = src.channel_two[x, y];
                    var blue = src.channel_three[x, y];

                    var min = Math.Min(Math.Min(red, green), blue);
                    saturation[x, y] = value[x, y] - min / value[x, y];
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
