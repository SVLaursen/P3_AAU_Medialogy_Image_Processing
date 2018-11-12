using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShapeDetector
{
    public static class CommandConsole
    {
        private static Color c;
        private static bool running;
        private const string versionNumber = "0.1";
        private static int _threshold = 50;
        private static string import = "test.png";
        private static string export = "image.bmp";
        
        private static readonly Dictionary<string, string> _command = new Dictionary<string, string>
        {
            {"-help", "Displays all commands"},
            {"-clear", "Clears the console"},
            {"-exit", "Exit the program"},
            {"-calibrate", "Calibrates the software"},
            {"-run", "Runs the shape detection software"},
            {"-colors", "Sets the colors for the detection software"},
            {"-import", "Sets the image import path"},
            {"-export", "Sets the file export name."},
            {"-threshold", "Sets the color threshold value"},
            {"-debug", "Runs system with default debugging values"}
        };

        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("MTA-18336 Image Processing Software; Version " + versionNumber);
            
            Console.WriteLine("\nCurrent Settings: \nThreshold: " + _threshold);
            Console.WriteLine("Import Path: " + import + "\nExport File Name: " + export+ "\n");
            
            Console.WriteLine("For list of commands write '-help'.");
            running = true;
            
            while (running) DetectInput(Console.ReadLine());
        }

        //public static List<Color> GetColors => _colors;

        private static void DetectInput(string input)
        {
            switch (input)
            {
                case "-help":
                    DisplayHelp();
                    break;
                case "-exit":
                    running = false;
                    break;
                case "-clear":
                    Console.Clear();
                    Run();
                    break;
                case "-colors":
                    SetColors();
                    break;
                case "-calibrate":
                    Calibration();
                    break;
                case "-run":
                    Program.StartProgram(c, _threshold, import, export);
                    break;
                case "-import":
                    Console.WriteLine("Set image import file::");
                    import = Console.ReadLine();
                    Console.WriteLine("New image import file: " + import);
                    break;
                case "-export":
                    Console.WriteLine("Set image export file:");
                    export = Console.ReadLine();
                    Console.WriteLine("New export file: " + export);
                    break;
                case "-threshold":
                    Console.WriteLine("Set threshold:");
                    var thresholdInput = Console.ReadLine();
                    if (int.TryParse(thresholdInput, out var value))
                    {
                        _threshold = value;
                        Console.WriteLine("New Threshold: " + _threshold);
                    }
                    else Console.WriteLine("--Invalid Input--");
                    break;
                case "-debug":
                    Debug();
                    break;
                default:
                    Console.WriteLine("--Invalid input--");
                    break;
            }
        }

        //This is the function used for manually adding colors to the list of detectable colors
        //Let's hope we don't have to use it and that we can automate as much as possible
        private static void SetColors()
        {
            var setting = true;

            while (setting)
            {
                Console.WriteLine("Please write the R-value");
                var red = SetVal(Console.ReadLine());

                Console.WriteLine("Please write the G-value");
                var green = SetVal(Console.ReadLine());

                Console.WriteLine("Please write the B-value");
                var blue = SetVal(Console.ReadLine());

                Console.WriteLine("Compiling color");
                _colors.Add(Color.FromArgb(red, green, blue));
            }

            int SetVal(string input)
            {
                if (int.TryParse(input, out var value)) return value;
                
                Console.WriteLine("-- Invalid Input -- \nValue automatically signed to 0");
                return 0;
            }
        }

        private static void Calibration()
        {
            //TODO: Calibration code get called from here.
            Console.WriteLine("Not yet implemented"); //Remove later
            //Console.WriteLine(CCHandler.DeltaEDebug(ImageHandler.LoadImage("test2.png"), Color.Black));
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("List of Commands: ");

            foreach (var t in _command)
            {
                Console.WriteLine("' " + t.Key+ " '" + " -- " + t.Value);
            }
        }

        private static void Debug()
        {
            Console.WriteLine("Running System Debug...");

            //Add debug colors to the debug color list
            var _c = new List<Color>(new[]{
                Color.FromArgb(255,0,0), Color.FromArgb(0,255,0), Color.FromArgb(0,0,255)});

            Program.StartProgram(_c, 250, "test2.png", "debug");
            Console.WriteLine("Debugging complete..");
        }
    }
}