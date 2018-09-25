//Includes
#include <iostream>
#include "opencv2/opencv.hpp"
#include <stdint.h>
#include <bitset>

//Namespaces
using namespace cv;
using namespace std;

//Variables
Mat original;

//Define countour vector
vector<vector<Point> > contours;

//Function declarations
Mat imageProcess(Mat original);


//Main function
int main(int argv, char** argc)
{
	VideoCapture videoStream(0);
	Mat image = imread("src.png");
	//imshow("image", ImageCapture(image));
	//namedWindow("Webcam");
	while (videoStream.isOpened()) {
		videoStream >> image;
		imshow("Webcam", imageProcess(image));
		waitKey(1);
	}
	
	return(0);	
}

Mat imageProcess(Mat original) {
	//imshow("original", original);
	//Variables for binary conversion
	double thr1 = 800;
	double thr2 = 850;

	//Hold pointers for contours
	vector<Point> result;
	vector<vector<Point> > contours;
	vector<Vec4i> hierarchy;

	//Variables for image storage
	Mat grayscale, binary;

	//Convert source image to grayscale
	cvtColor(original, grayscale, CV_RGB2GRAY);

	//Convert grayscale image to binary
	Canny(grayscale, binary, thr1, thr2, 5, true);
	imshow("Binary", binary);
	imshow("Grayscale", grayscale);
	//Find contours of the binary image
	findContours(binary, contours, hierarchy, RETR_EXTERNAL, CHAIN_APPROX_SIMPLE);
	
	for (int i = 0; i < contours.size(); i++) {
		approxPolyDP(contours[i], result, arcLength(contours[i], true)*0.2, true);
	}

	//If the contour is a triangle
	if (result.size() == 3) {
		//Iterate through each point

		line(original, result.at(0), result.at(1), cvScalar(255, 0, 255), 4);
		line(original, result.at(1), result.at(2), cvScalar(255, 0, 255), 4);
		line(original, result.at(2), result.at(0), cvScalar(255, 0, 255), 4);

	}

	//If the contour is a quadrilateral
	if (result.size() == 4) {
		//Iterate through each point

		line(original, result.at(0), result.at(1), cvScalar(0, 0, 255), 4);
		line(original, result.at(1), result.at(2), cvScalar(0, 0, 255), 4);
		line(original, result.at(2), result.at(3), cvScalar(0, 0, 255), 4);
		line(original, result.at(3), result.at(0), cvScalar(0, 0, 255), 4);
	}

	//If the contour is a pentagon
	if (result.size() == 5) {
		//Iterate through each point

		line(original, result.at(0), result.at(1), cvScalar(0, 255, 255), 4);
		line(original, result.at(1), result.at(2), cvScalar(0, 255, 255), 4);
		line(original, result.at(2), result.at(3), cvScalar(0, 255, 255), 4);
		line(original, result.at(3), result.at(4), cvScalar(0, 255, 255), 4);
		line(original, result.at(4), result.at(0), cvScalar(0, 255, 255), 4);
	}

	//if the contour is a hexagon
	if (result.size() == 6) {
		//Iterate through each point

		line(original, result.at(0), result.at(1), cvScalar(255, 255, 0), 4);
		line(original, result.at(1), result.at(2), cvScalar(255, 255, 0), 4);
		line(original, result.at(2), result.at(3), cvScalar(255, 255, 0), 4);
		line(original, result.at(3), result.at(4), cvScalar(255, 255, 0), 4);
		line(original, result.at(4), result.at(5), cvScalar(255, 255, 0), 4);
		line(original, result.at(5), result.at(0), cvScalar(255, 255, 0), 4);
	}
	//Obtain a sequence of contours


	//Add more for more shapes


	return(original);
}
