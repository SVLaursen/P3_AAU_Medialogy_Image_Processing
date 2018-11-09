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


        public float[,] GetHue(Bitmap img)
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
                        hue[y, x] = (green - blue) / (value[y, x] - min) * degrees;
                    
                    else if (green == value[y, x]) 
                        hue[y, x] = ((blue - red) / (value[y, x] - min) + 2) * degrees;
                    
                    else if (blue == value[y, x]) 
                        hue[y, x] = ((red - green) / (value[y, x] - min) + 4) * degrees;
                    
                    else if (value[y, x] == red && green < blue) 
                        hue[y, x] = ((red - blue) / (value[y, x] - min) + 5) * degrees;
                }
            }

            return hue;
        }

        public float[,] GetValue(Bitmap img)
        {
            var src = Split(img);
            var value = new float[img.Height, img.Width];
            
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

        public float[,] GetSaturation(Bitmap img)
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

        public float[,] GetIntensity(Bitmap img)
        {
            var src = Split(img);
            var intensity = new float[img.Height, img.Width];

            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    var red = src.channel_one[y, x];
                    var green = src.channel_two[y, x];
                    var blue = src.channel_three[y, x];

                    intensity[y, x] = 1 / 3 + (red + green + blue);
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
                    var red = (int)splitImg.channel_one[y, x];
                    var green = (int)splitImg.channel_two[y, x];
                    var blue = (int)splitImg.channel_three[y, x];

                    var clr = Color.FromArgb(red, green, blue);
                    bmp.SetPixel(y, x, clr);
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