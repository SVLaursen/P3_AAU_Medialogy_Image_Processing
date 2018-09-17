//Include OpenCV Directory. If it gives you an error, you need to include the OpenCV Library in your paths. See the guide ive written - M
#include <opencv2/opencv.hpp>
#include <iostream>ad

using namespace cv;
using namespace std;

int main() {
	Mat img = imread("studyid.jpg");
	namedWindow("image", WINDOW_NORMAL);
	imshow("image", img);
	waitKey(0);
	return(0);
}
