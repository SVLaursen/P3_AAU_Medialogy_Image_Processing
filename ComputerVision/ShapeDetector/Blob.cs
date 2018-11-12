using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

namespace ShapeDetector
{
    public class Blob
    {
        //Class Variables
        public Vector2 maxP;
        public Vector2 minP;
        private List<Vector2> points = new List<Vector2>();
        private Color _c;

        //Public constructor
        public Blob(int _x, int _y, Color c)
        {
            _c = c;
            maxP = new Vector2(_x, _y);
            minP = new Vector2(_x, _y);
            points.Add(new Vector2(_x, _y));
        }

        //Determines wether the blob is withing a certain distance of a specified point
        public bool isNear(int _x, int _y, int _threshold)
        {
            Vector2 point = new Vector2(_x, _y);
            double d = double.MaxValue;

            Parallel.ForEach(points, v =>
            {
                float _d = Vector2.DistanceSquared(point, v);
                if (_d < d)
                {
                    d = _d;
                }
            });

            if (d < Math.Pow(_threshold, 2)) { return true; }
            else { return false; }
        }

        //Returns the blobs distance to a specified point
        public float DistanceTo(int _x, int _y)
        {
            Vector2 point = new Vector2(_x, _y);
            double d = double.MaxValue;

            foreach (Vector2 v in points)
            {
                float _d = Vector2.DistanceSquared(point, v);
                if (_d < d)
                {
                    d = _d;
                }
            }
            return (float)d;
        }
        //Adds a new pixel to the blob
        public void Add(int newx, int newy)
        {
            points.Add(new Vector2(newx, newy));
            minP.X = Math.Min(maxP.X, newx);
            minP.Y = Math.Min(maxP.Y, newy);
            maxP.X = Math.Max(maxP.X, newx);
            maxP.Y = Math.Max(maxP.Y, newy);
        }

        //Returns an Array of the Blob Corners in a clockwise rotation
        public Point[] getCorners()
        {
            Point[] corners = new Point[4];
            corners[0] = new Point((int)minP.X, (int)minP.Y);
            corners[1] = new Point((int)minP.X, (int)maxP.Y);
            corners[2] = new Point((int)maxP.X, (int)maxP.Y);
            corners[3] = new Point((int)maxP.X, (int)minP.Y);

            return corners;
        }

        //Returns the blob color;
        public Color GetColor()
        {
            return _c;
        }
        public static void DrawBlobs(Bitmap _img, List<Blob> _b)
        {
            Pen pen = new Pen(Color.White, 5);
            var graphics = Graphics.FromImage(_img);

            foreach (Blob b in _b)
            {
                Point[] k = b.getCorners();
                graphics.DrawLine(pen, k[0], k[1]);
                graphics.DrawLine(pen, k[1], k[2]);
                graphics.DrawLine(pen, k[2], k[3]);
                graphics.DrawLine(pen, k[3], k[0]);
          
            }
            graphics.DrawImage(_img, _img.Height, _img.Width);
        }

    }
}
