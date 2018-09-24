cmake_minimum_required(VERSION 3.12)
project(P3_AAU_Medialogy_Image_Processing)

set(CMAKE_CXX_STANDARD 11)

find_package( OpenCV REQUIRED )
set(SOURCE_FILES main.cpp)
add_executable(ThirdSemesterProject ${SOURCE_FILES})
include_directories(${OpenCV_INCLUDE_DIRS})
target_link_libraries( ThirdSemesterProject ${OpenCV_LIBS} )

add_executable(P3_AAU_Medialogy_Image_Processing main.cpp)