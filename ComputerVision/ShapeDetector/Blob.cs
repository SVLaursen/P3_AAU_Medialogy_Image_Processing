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
        public List<Vector2> points = new List<Vector2>();

        //Public constructor
        public Blob(int _x, int _y)
        {
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
                float _d = Vector2.Distance(point, v);
                if (_d < d)
                {
                    d = _d;
                }
            });

            if (d < _threshold) { return true; }
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
            for(int i = 0; i < corners.Length; i++)
            {
                corners[i] = new Point(-1, -1);
            }
            foreach (Vector2 pixel in points)
            {
                //Topmost corner
                if (corners[0].Y != -1)
                {
                    if (corners[0].Y > pixel.Y)
                    {
                        corners[0].Y = (int)pixel.Y;
                        corners[0].X = (int)pixel.X;
                    }
                }
                else
                {
                    corners[0].Y = (int)pixel.Y;
                    corners[0].X = (int)pixel.X;
                }
                //Leftmost corner
                if (corners[1].X != -1)
                {
                    if (corners[1].X >= pixel.X)
                    {
                        corners[1].Y = (int)pixel.Y;
                        corners[2].X = (int)pixel.X;
                    }
                }
                else
                {
                    corners[1].Y = (int)pixel.Y;
                    corners[1].X = (int)pixel.X;
                }

                //Bottom corner
                if (corners[2].Y != -1)
                {
                    if (corners[2].Y <= pixel.Y)
                    {
                        corners[2].Y = (int)pixel.Y;
                        corners[2].X = (int)pixel.X;
                    }
                }
                else
                {
                    corners[2].Y = (int)pixel.Y;
                    corners[2].X = (int)pixel.X;
                }

                //Rightmost corner
                if (corners[3].X != -1)
                {
                    if (corners[3].X <= pixel.X)
                    {
                        corners[3].Y = (int)pixel.Y;
                        corners[3].X = (int)pixel.X;
                    }
                }
                else
                {
                    corners[3].Y = (int)pixel.Y;
                    corners[3].X = (int)pixel.X;
                }
            }
            return corners;
        }

        //Draws lines between 4 points supplied in a point array
        public void DrawCorners(Bitmap _img, Point[] points)
        {
            Pen pen = new Pen(Color.White, 2);
            var graphics = Graphics.FromImage(_img);

            graphics.DrawLine(pen, points[0], points[1]);
            graphics.DrawLine(pen, points[1], points[2]);
            graphics.DrawLine(pen, points[2], points[3]);
            graphics.DrawLine(pen, points[3], points[0]);
            graphics.DrawImage(_img, _img.Height, _img.Width);
        }

    }
}
