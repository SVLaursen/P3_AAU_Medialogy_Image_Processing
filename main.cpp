//Includes
#include <iostream>
#include "opencv2/opencv.hpp"
#include <stdint.h>
#include <thread>
#include <cstdlib>

//Namespaces
using namespace cv;
using namespace std;

//Variables
Mat original;
double thr1;
double thr2;
string configureCmd;
bool cam1 = true;
bool cam2, cam3 = false;
Mat image;
Mat storedImage;
float epsilon;

//Define countour vector
vector<vector<Point> > contours;

//Function declarations
Mat imageProcess(Mat original);
void cmdPrompt();
void initialize();
void thresholdChange();
void cameras();
void epsilonChange();

//Main function
int main(int argv, char** argc)
{
	initialize();
	VideoCapture videoStream(0);
	//_beginthread(cmdPrompt, NULL, NULL);
	thread cmdThread(cmdPrompt);
	while (videoStream.isOpened()) {
		videoStream >> image;
		storedImage = imageProcess(image);
		if(cam1 == true) imshow("Output", storedImage);
		waitKey(1);
	}
	cmdThread.join();
	return(0);
}
void cmdPrompt() {
	while (true) {
		system("CLS");
		cout << "MEA-18336 Shape Detection Software.\n";
		cout << "\nCurrent Settings:\nThreshold 1: " << thr1 << "\nThreshold 2: " << thr2 << "\nEpsilon: " << epsilon << "\n";
		cout << "\nPossible Settings: \n: cameras : Enable or Disable Camera Windows \n: threshold : Adjust binary threshold\n: epsilon : Adjust ApproxPoly epsilon\n: exit : Exits the application\n: ";
		string input;
		cin >> input;
		if (input == "threshold") {
			system("CLS");
			thresholdChange();
		}
		if (input == "cameras") {
			system("CLS");
			cameras();
		}
		if (input == "epsilon") {
			system("CLS");
			epsilonChange();
		}
		if (input == "exit") {
			exit(0);
		}
		else {
			system("CLS");
		}
	}
}
void cameras() {
	int nmbr;
	bool loop = false;
	loop = true;
	while (loop) {
		cout << "Enable Cameras:\n1. Processed Output\n2. Grayscale\n3. Binary\n4. Back\n";
		cin >> nmbr;
		if (nmbr == 1) {
			if (cam1 == true) {
				cam1 = false;
				destroyWindow("Output");
			}
			else cam1 = true;
			system("CLS");
		}
		if (nmbr == 2) {
			if (cam2 == true) {
				cam2 = false;
				destroyWindow("Grayscale");
			}
			else cam2 = true;
			system("CLS");
		}
		if (nmbr == 3) {
			if (cam3 == true) { 
				cam3 = false;
				destroyWindow("Binary");
			}
			else cam3 = true;
			system("CLS");
		}
		if (nmbr == 4) {
			loop = false;
			system("CLS");
		}
	}
}
void initialize() {
	cout << "MEA-18336 Shape Detection Software.\n";
	cout << "\nWelcome. Please intialize Canny edge detection systems by inputting the threshold limits and epsilon.\n";
	cout << "|------------------------------------------------------------------------------------------|\n";
	cout << "Set threshold 1: ";
	cin >> thr1;
	cout << "Threshold 1 set to: ";
	cout << thr1;
	cout << "\nSet threshold 2: ";
	cin >> thr2;
	cout << "Threshold 2 set to: ";
	cout << thr2;
	cout << "\nSet Epsilon: ";
	cin >> epsilon;
	cout << "Epsilon set to: ";
	cout << epsilon;
	cout << "\n....\nInitialization finished!\n\nCAMERA STARTING\n";
	cout << "|------------------------------------------------------------------------------------------\n";
	waitKey(1000);
}
void thresholdChange() {
		waitKey(1);
		cout << "MEA-18336 Shape Detection Software.\n";
		cout << "\nSet Threshold 1: ";
		cin >> thr1;
		cout << "\nThreshold 1 set to: ";
		cout << thr1;
		cout << "\nSet Threshold 2: ";
		cin >> thr2;
		cout << "\nThreshold 2 set to: ";
		cout << thr2;
		waitKey(1000);
		system("CLS");
	}
void epsilonChange() {
	waitKey(1);
	cout << "MEA-18336 Shape Detection Software.\n";
	cout << "\nSet Epsilon: ";
	cin >> epsilon;
}
Mat imageProcess(Mat original) {
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
	if(cam3 == true)imshow("Binary", binary);
	if(cam2 == true)imshow("Grayscale", grayscale);
	//Find contours of the binary image
	findContours(binary, contours, hierarchy, RETR_LIST, CHAIN_APPROX_SIMPLE);

	for (int i = 0; i < contours.size(); i++) {
		approxPolyDP(contours[i], result, epsilon, true);
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
