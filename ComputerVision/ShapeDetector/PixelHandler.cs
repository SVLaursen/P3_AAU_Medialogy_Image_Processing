using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ShapeDetector
{


    class PixelHandler
    {
        private Bitmap _img;
        private Rectangle rect;
        private BitmapData imgData;
        private IntPtr ptr;
        private int bytes;
        private byte[] pixels;
        private int BPP;
        private int stride;
        private int widthInBytes;

        ///<summary>Class made to handle pixels fast.
        ///The regular Bitmap.GetPixel / Bitmap.SetPixel is slow for the purpose that we use it for. 
        ///To use the PixelHandler, create a new instance of the PixelHandler with the image that you want to modify.Then store the BytesPerPixel in an int variable. You will need this.
        ///When iterating through your bitmap, do it y, _x, where _x is equal to pixelHandler.Width().
        ///Inside the nested for-loop, declare int x = _x / bpp, and use x instead of _x in any x variable that is not associated with PixelHandler. This could be an array or something similar.
        ///When you are done modifying your image, call the Return() function, and then call Close() on any image that has been used, but doesnt need to be returned.</summary>
        public PixelHandler(Bitmap img)
        {
            _img = img;
            rect = new Rectangle(0, 0, _img.Width, _img.Height);
            imgData = _img.LockBits(rect, ImageLockMode.ReadWrite, img.PixelFormat);
            stride = imgData.Stride;

            bytes = stride * _img.Height;
            pixels = new byte[bytes];

            ptr = imgData.Scan0;
            Marshal.Copy(ptr, pixels, 0, pixels.Length);

            BPP = Bitmap.GetPixelFormatSize(img.PixelFormat) / 8;
            widthInBytes = imgData.Width * BPP;
        }

        ///<summary>Returns Width in Bytes</summary>
        public int Width()
        {
            return widthInBytes;
        }

        ///<summary>Returns Pixel Color</summary>
        public Color GetPixel(int x, int y)
        {
            int line = y * stride;
            return Color.FromArgb(pixels[line + x + 2], pixels[line + x + 1], pixels[line + x]);
        }
        ///<summary>Sets Pixel Color</summary>
        public void SetPixel(int x, int y, Color c)
        {
            int line = y * stride;
            pixels[line + x + 2] = (byte)c.R;
            pixels[line + x + 1] = (byte)c.G;
            pixels[line + x] = (byte)c.B;
        }
        ///<summary>Unlocks the Bitmap bits and returns the processed Bitmap</summary>
        public Bitmap Return()
        {

            Marshal.Copy(pixels, 0, ptr, pixels.Length);
            _img.UnlockBits(imgData);
            return _img;
        }
        ///<summary>Unlocks the Bitmap bits. If processed image is needed, use Return() instead</summary>
        public void Close()
        {
            _img.UnlockBits(imgData);
        }

        /// <summary>Returns BytesPerPixel</summary>
        public int BytesPerPixel()
        {
            return BPP;
        }

    }
}
