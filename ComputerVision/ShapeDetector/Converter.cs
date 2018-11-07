using System;
using System.Drawing;

namespace ShapeDetector
{
    public class Converter
    {

        public void RGB2HSV(Bitmap img)
        {
            Console.WriteLine("This function has not been implemented");
            //TODO: Make full image conversion
        }


        private float[,] GetHue(Bitmap img)
        {
            var value = GetValue(img);
            var src = Split(img);
            var hue = new float[img.Height, img.Width];

            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    var red = src.channel_one[y, x];
                    var green = src.channel_two[y, x];
                    var blue = src.channel_three[y, x];

                    const float degrees = 60 * (float)Math.PI / 180;
                    var min = Math.Min(Math.Min(red, green), blue);

                    if (value[y, x] == red && green >= blue)
                    {
                        hue[y, x] = (green - blue) / (value[y, x] - min) * degrees;
                    }
                    else if (green == value[y, x])
                    {
                        hue[y, x] = ((blue - red) / (value[y, x] - min) + 2) * degrees;
                    }
                    else if (blue == value[y, x])
                    {
                        hue[y, x] = ((red - green) / (value[y, x] - min) + 4) * degrees;
                    }
                    else if (value[y, x] == red && green < blue)
                    {
                        hue[y, x] = ((red - blue) / (value[y, x] - min) + 5) * degrees;
                    }
                }
            }

            return hue;
        }

        private int[,] GetValue(Bitmap img)
        {
            var src = Split(img);
            var value = new int[img.Height, img.Width];
            
            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    value[y, x] = Math.Max(Math.Max(src.channel_one[y, x], src.channel_two[y, x]),
                        src.channel_three[y, x]);
                }
            }

            return value;
        }

        private float[,] GetSaturation(Bitmap img)
        {
            var src = Split(img);
            var value = GetValue(img);
            var saturation = new float[img.Height, img.Width];

            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    var red = src.channel_one[y, x];
                    var green = src.channel_two[y, x];
                    var blue = src.channel_three[y, x];

                    var min = Math.Min(Math.Min(red, green), blue);
                    saturation[y, x] = value[y, x] - min / value[y, x];
                }
            }
            
            return saturation;
        }

        public ImageChannels Split(Bitmap img)
        {
            var red = new int[img.Width, img.Height];
            var green = new int[img.Width, img.Height];
            var blue = new int[img.Width, img.Height];
            
            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    var clr = img.GetPixel(y, x);
                    
                    red[y, x] = clr.R;
                    green[y, x] = clr.G;
                    blue[y, x] = clr.B;
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
                    var red = splitImg.channel_one[y, x];
                    var green = splitImg.channel_two[y, x];
                    var blue = splitImg.channel_three[y, x];

                    var clr = Color.FromArgb(red, green, blue);
                    bmp.SetPixel(y, x, clr);
                }
            }
            
            return bmp;
        }
    }

    public struct ImageChannels
    {
        public int[,] channel_one;
        public int[,] channel_two;
        public int[,] channel_three;

        public int height;
        public int width;

        public void SetAllValues(int[,] c_one, int[,] c_two, int[,] c_three)
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
            channel_one = new int[height, width];
            channel_two = new int[height, width];
            channel_three = new int[height, width];
        }

        public void SetSize(int[,] size)
        {
            channel_one = size;
            channel_two = size;
            channel_three = size;
        }
    }
}