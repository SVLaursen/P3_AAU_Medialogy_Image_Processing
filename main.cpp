//Includes
#include <iostream>
#include "opencv2/opencv.hpp"
#include <stdint.h>
#include <bitset>

//Namespaces
using namespace cv;
using namespace std;

//Variables
VideoCapture videoStream(0);

Mat original, grayscale, binary;
Mat output;

double thr1 = 800;
double thr2 = 850;
int maxVal = 255;

vector<Point> result;
vector<vector<Point> > contours;

//Define countour vector
vector<Point> contoursConvexHull(vector<vector<Point> > contours)
{
    vector<Point> result;
    vector<Point> pts;
    for (size_t i = 0; i < contours.size(); i++)
        for (size_t j = 0; j < contours[i].size(); j++)
            pts.push_back(contours[i][j]);
    convexHull(pts, result);
    return result;
}

Mat ImageDetection(){
    //Load a frame from the camera
    videoStream.read(original);

    //Convert source image to grayscale
    cvtColor(original, grayscale, CV_RGB2GRAY);

    //Convert grayscale image to binary
    Canny(grayscale, binary, thr1, thr2, 5, true);

    //Find contours of the binary image
    findContours(binary, contours, RETR_LIST, CHAIN_APPROX_SIMPLE);
    for (int i = 0; i < contours.size(); i++) {

        approxPolyDP(Mat(contours[i]), result, arcLength(Mat(contours[i]), true)*0.01, true);

        //If the contour is a triangle
        if (result.size() == 3) {
            //Iterate through each point
            CvPoint* point[3]; //THIS IS A POINTER

            line(original, result.at(0), result.at(1), cvScalar(255, 0, 0), 4);
            line(original, result.at(1), result.at(2), cvScalar(255, 0, 0), 4);
            line(original, result.at(2), result.at(3), cvScalar(255, 0, 0), 4);
        }

        //If the contour is a quadrilateral
        if (result.size() == 4) {

        }

        //If the contour is a pentagon
        if (result.size() == 5) {


        }

        //if the contour is a hexagon
        if (result.size() == 6) {

        }
    }
    //Obtain a sequence of contours


    //Add more for more shapes

    return original;
}

bool OutputActive(Mat output){
    if(countNonZero(output) < 1){
        return false;
    }
    return true;
}

//Main function
int main(int argv, char** argc)
{
    while (true) {
        if(!OutputActive(output)) break;
        imshow("Output", ImageDetection());
    }

    return(0);
}