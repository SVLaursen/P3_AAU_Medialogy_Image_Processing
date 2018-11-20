using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ShapeDetector
{
    internal class ImageHandler
    {
        private Capture _capture = null;
        private bool _captureInProgress;
        private int CameraDevice = 0; //This is for testing purposes, as 'O' is the default camera (Built in Webcam), we will chnage this later on.
        private Video_Device[] Webcams;

        //Returns a bitmap with the loaded image
        public static Bitmap LoadImage(string filepath)
        {
            var _bitmap = (Bitmap)Bitmap.FromFile(filepath);
            
            return _bitmap ?? null;
        }

        private void SetupCapture(int Camera_Identifier)
        {
            CameraDevice = Camera_Identifier;

            if (_capture != null) _capture.Dispose();
            try
            {
                //set up capture device
                _capture = new Capture(CameraDevice);
                _capture.ImageGrabbed += ProcessFrame;
            }
            catch (NullReferenceException e);
            {
                Console.WriteLine(e);
            }
        }
        
        public void InitializeCamera(); //Initializing the Camera 
        {
            DsDevice[] _SystemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            Webcams = new Video_Device[_SystemCameras.Length];
            for (int i = 0; i < _SystemCameras.Length; i++)
            {
                Webcams[i] = new Video_Device(i, _SystemCameras[i].Name, _SystemCameras[i].ClassID);
            }
            
        }

        private Bitmap ProcessFrame()
        {
            Image<Bgr, Byte> frame = _capture.RetreivedBgrFrame();
            Bitmap BmpInput = frame.ToBitmap();
            return BmpInput;
        }
        
        private void Capture()
        {
            if (_capture != null)
            {
                if (Camera_Selection.Selected.Index != CameraDevice)
                {
                    SetupCapture(Camera_Selection.SelectedIndex);
                }

                _capture.Start();
                _captureInProgress = true;
            }
            else
            {
                //set up capture with selected device
                SetupCapture((Camera_Selection.SelectedIndex));
                //Be lazy and recall this method to start the camera
                Capture();
            }            
        }          
    }    
}

