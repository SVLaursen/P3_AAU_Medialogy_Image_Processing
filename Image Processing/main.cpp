#include <opencv2/opencv.hpp>
using namespace cv;

int main(int argv, char** argc)
{
	Mat test = imread("testpat.jpg", CV_LOAD_IMAGE_UNCHANGED);
	imshow("Test", test);
	waitKey();
}
