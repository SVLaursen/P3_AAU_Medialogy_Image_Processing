using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace ShapeDetector
{
    public static class CommandConsole
    {
        private static bool running;
        private const string versionNumber = "0.2";
        private static int _threshold = 0;
        private static string import = "test2.png";
        private static string export = "image.bmp";
        private static Color c = Color.White;
        
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
                    //Program.StartProgram(_threshold, import, export);
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
                //_colors.Add(Color.FromArgb(red, green, blue));
                Console.WriteLine("Add another color? Y/N");
                String k = Console.ReadLine();
                if (k == "Y" || k == "y")
                {
                    setting = false;
                    SetColors();
                }
                else if(k == "N" || k == "n")
                {
                    setting = false;
                    Console.WriteLine("COMPLETE - Returning to main menu");
                    
                }
                else
                {
                    Console.WriteLine("--Invalid Input--");
                    Console.WriteLine("Returning to main menu");
                    setting = false;
                }


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
            Stopwatch stop = new Stopwatch();
            stop.Start();
            Console.WriteLine("Running System Debug...");

            Program.StartProgram("test44.bmp", "test4.bmp", "debug3.bmp");
            //Program.StartProgram(25, "test2.png", "debug2.bmp");
            stop.Stop();
            Console.WriteLine(" Debugging time: " + stop.ElapsedMilliseconds + " Milliseconds");
            Console.WriteLine("\nDebugging complete..");
        }
    }
}