using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeDetector
{
    class Grassfire
    {
        private Converter _converter = new Converter();
       
        //TODO: Modify blackThresholdi(ing) so that it takes a range instead of a hard crop. 
        //TODO: This system will look for bright objects, we want the opposite. For quick fix, flip signs where blackThreshold is used. Range modification will also do.
        //Runs a grassfire algorithm within an area of an image, and returns an int channel with BLOBS. If returnOnlyAreaOfInterest is true, the result is only the size of the area examined, else the result is the same size as the original image.
        public int[,] RunGrassfire(Bitmap img, int xMin, int xMax, int yMin, int yMax, int blackThreshold, int colorThreshold, bool returnOnlyAreaOfInterest)
        {
            float[,] intensity = _converter.GetIntensity(img);

            int[,] result = new int[
                returnOnlyAreaOfInterest ? xMax : img.Width,
                returnOnlyAreaOfInterest ? yMax : img.Height];

            FindAllObjects();
            return result;

            //Looks for pixels with greater intensity than the threshold
            void FindAllObjects()
            {
                //NOTE: objectCounter increases by 20 each time; an arbitrary number. We could use anything here, really.
                var objectCounter = 0;

                for (var y = yMin; y < yMax; y++)
                {
                    for (var x = xMin; y < xMax; x++)
                    {
                        float pixelIntensity = intensity[x, y];
                        if (pixelIntensity > blackThreshold)
                        {
                            objectCounter += 20;
                            CheckConnectivity(x, y, pixelIntensity);
                        }
                    }
                }

                void CheckConnectivity(int x, int y, float color)
                {
                    //NOTE: Recursive calls use the original color (intensity) that started the recursion. Theoretically, it wouldn't be able to handle gradients..?
                    //Set current information - Current intensity pixel is set to zero to avoid registering it multiple times, and current result pixel is given current object number
                    intensity[x, y] = 0;
                    result[
                        returnOnlyAreaOfInterest ? x - xMin : x,
                        returnOnlyAreaOfInterest ? y - yMin : y] = objectCounter;

                    //Check neighbors
                    //right
                    if (x + 1 < xMax)
                    {
                        float right = intensity[x + 1, y];
                        if (right <= color + colorThreshold && right >= color - colorThreshold)
                        {
                            CheckConnectivity(x + 1, y, color);
                        }
                    }
                    //bottom
                    if (y + 1 < yMax)
                    {
                        float bottom = intensity[x, y + 1];
                        if (bottom > color + colorThreshold && bottom > color - colorThreshold)
                        {
                            CheckConnectivity(x, y + 1, color);
                        }
                    }
                    //left
                    if (x - 1 < xMin)
                    {
                        float left = intensity[x - 1, y];
                        if (left <= color + colorThreshold && left >= color - colorThreshold)
                        {
                            CheckConnectivity(colorThreshold - 1, y, color);
                        }
                    }
                    //top
                    if (y - 1 < yMin)
                    {
                        float top = intensity[x, y - 1];
                        if (top <= color + colorThreshold && top >= color - colorThreshold)
                        {
                            CheckConnectivity(x, y + 1, color);
                        }
                    }
                }
            }
        }

        //Runs a grassfire algorithm on an entire image, and returns an int channel with BLOBS
        public int[,] RunGrassfire(Bitmap img, int blackThreshold, int colorThreshold)
        {
            return RunGrassfire(img, 0, img.Width, 0, img.Height, blackThreshold, colorThreshold, false);
        }
    }
}

//Original python source, for reference
/*def findAllObjects(yMin, yMax, xMin, xMax, blackTH, colorTH):
    objectCounter = 0

    for y in range(yMin, yMax):
        for x in range(xMin, xMax):
            if img[y, x] > blackTH:
                objectCounter += 20
                checkConnectivity(y, yMin, yMax, x, xMin, xMax, img[y, x], colorTH, objectCounter)



def checkConnectivity(y, yMin, yMax, x, xMin, xMax, color, colorTH, objectNumber):

    img[y, x] = 0
    objectImage[y, x] = objectNumber

    if x + 1 < xMax:
        right = img[y, x + 1]
        if right <= color + colorTH:
            if right >= color - colorTH:
                checkConnectivity(y, yMin, yMax, x + 1, xMin, xMax, color, colorTH, objectNumber)

    if y + 1 < yMax:
        bottom = img[y + 1, x]
        if bottom <= color + colorTH:
            if bottom >= color - colorTH:
                checkConnectivity(y + 1, yMin, yMax, x, xMin, xMax, color, colorTH, objectNumber)

    if x - 1 > xMin:
        left = img[y, x - 1]
        if left <= color + colorTH:
            if left >= color - colorTH:
                checkConnectivity(y, yMin, yMax, x - 1, xMin, xMax, color, colorTH, objectNumber)

    if y - 1 > yMin:
        top = img[y - 1, x]
        if top <= color + colorTH:
            if top >= color - colorTH:
                checkConnectivity(y - 1, yMin, yMax, x, xMin, xMax, color, colorTH, objectNumber)
*/
