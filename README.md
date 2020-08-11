# 3rd Semester Medialogy Project - Image Processing
Third semester project - MEA18336 Aalborg University

This project was made by a group of 6 people. The goal of the project was to produce an image processing software that would interact with a game which would then be projected onto a table from which the image processing algorithm would get its data.

The process of this software is as follows:
- Image processing software takes a picture of the tabletop with no elements placed on it.
- Real life elements gets placed on the table and the software takes another picture.
- The software runs a background substraction algorithm to remove any elements that are present in both pictures.
- Once the previous process is done, a BLOB detection algorithm detects any BLOBS left behind, using a CCL approach.
- The software finishes up by cleaning the colours of the BLOBS to their extreme values and then exports the data collected in a byte file

## What I Learned
- How to combine image processing algorithms and their usage
- Image cleaning through BLOB detection and RGB cleaning
- How to connect an external program with another (in this instance Unity) through byte data
