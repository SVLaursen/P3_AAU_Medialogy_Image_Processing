# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 3.12

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


# Remove some rules from gmake that .SUFFIXES does not remove.
SUFFIXES =

.SUFFIXES: .hpux_make_needs_suffix_list


# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /Applications/CLion.app/Contents/bin/cmake/mac/bin/cmake

# The command to remove a file.
RM = /Applications/CLion.app/Contents/bin/cmake/mac/bin/cmake -E remove -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing"

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing/cmake-build-debug"

# Include any dependencies generated for this target.
include CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/depend.make

# Include the progress variables for this target.
include CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/progress.make

# Include the compile flags for this target's objects.
include CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/flags.make

CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.o: CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/flags.make
CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.o: ../main.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir="/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing/cmake-build-debug/CMakeFiles" --progress-num=$(CMAKE_PROGRESS_1) "Building CXX object CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.o"
	/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.o -c "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing/main.cpp"

CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.i"
	/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing/main.cpp" > CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.i

CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.s"
	/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing/main.cpp" -o CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.s

# Object files for target P3_AAU_Medialogy_Image_Processing
P3_AAU_Medialogy_Image_Processing_OBJECTS = \
"CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.o"

# External object files for target P3_AAU_Medialogy_Image_Processing
P3_AAU_Medialogy_Image_Processing_EXTERNAL_OBJECTS =

P3_AAU_Medialogy_Image_Processing: CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/main.cpp.o
P3_AAU_Medialogy_Image_Processing: CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/build.make
P3_AAU_Medialogy_Image_Processing: CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir="/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing/cmake-build-debug/CMakeFiles" --progress-num=$(CMAKE_PROGRESS_2) "Linking CXX executable P3_AAU_Medialogy_Image_Processing"
	$(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/build: P3_AAU_Medialogy_Image_Processing

.PHONY : CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/build

CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/clean:
	$(CMAKE_COMMAND) -P CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/cmake_clean.cmake
.PHONY : CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/clean

CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/depend:
	cd "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing/cmake-build-debug" && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing" "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing" "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing/cmake-build-debug" "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing/cmake-build-debug" "/Users/simonlaursen/Dropbox/AAU/3Semester/Semester Project/Prototype/P3_AAU_Medialogy_Image_Processing/cmake-build-debug/CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/DependInfo.cmake" --color=$(COLOR)
.PHONY : CMakeFiles/P3_AAU_Medialogy_Image_Processing.dir/depend
