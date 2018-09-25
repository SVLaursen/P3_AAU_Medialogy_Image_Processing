//Includes
#include <iostream>
#include "opencv2/opencv.hpp"
#include <stdint.h>
#include <thread>
#include <process.h>

//Namespaces
using namespace cv;
using namespace std;

//Variables
Mat original;
double thr1;
double thr2;
string configureCmd;
//Define countour vector
vector<vector<Point> > contours;

//Function declarations
Mat imageProcess(Mat original);
void thresholdStart();
void thresholdChange(void* nmbr);

//Main function
int main(int argv, char** argc)
{
	thresholdStart();
	_beginthread(thresholdChange, NULL, NULL);
	VideoCapture videoStream(0);
	Mat image = imread("src.png");
	//imshow("image", ImageCapture(image));
	//namedWindow("Webcam");
	while (videoStream.isOpened()) {
		videoStream >> image;
		imshow("Webcam", imageProcess(image));
		waitKey(1);
	}
	_endthread();
	return(0);
}

void thresholdStart() {
	cout << "Welcome. Please intialize Canny edge detection systems by inputting the threshold limits.\n";
	cout << "|-----------------------------------------------------------------------------------------------------------------------\n";
	cout << "Set threshold 1: ";
	cin >> thr1;
	cout << "Threshold 1 set to: ";
	cout << thr1;
	cout << "\nSet threshold 2: ";
	cin >> thr2;
	cout << "Threshold 2 set to: ";
	cout << thr2;
	cout << "\n....\nInitialization finished!\n\nThreshold is changeable during runtime. \n\nCAMERA STARTING\n";
	cout << "|-----------------------------------------------------------------------------------------------------------------------\n";
	waitKey(1000);

}
void thresholdChange(void* nmbr) {
	while (1) {
		waitKey(1);
		cout << "|-----------------------------------------------------------------------------------------------------------------------\n";
		cout << "\nSet Threshold 1: ";
		cin >> thr1;
		cout << "\nThreshold 1 set to: ";
		cout << thr1;
		cout << "\nSet Threshold 2: ";
		cin >> thr2;
		cout << "\nThreshold 2 set to: ";
		cout << thr2;
	}
}
Mat imageProcess(Mat original) {
	//imshow("original", original);
	//Variables for binary conversion

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
