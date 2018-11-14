using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace ShapeDetector
{
    public static class CommandConsole
    {
        private static bool running;
        private const string versionNumber = "0.4 DEBUG";
        private static string import = "test2.png";
        private static string export = "image.bmp";
        
        private static int _threshold = -1;
        private static int _colorThreshold = -1;
        
        private static readonly Dictionary<string, string> _command = new Dictionary<string, string>
        {
            {"-help", "Displays all commands"},
            {"-clear", "Clears the console"},
            {"-exit", "Exit the program"},
            {"-calibrate", "Calibrates the software"},
            {"-run", "Runs the shape detection software"},
            {"-colorThreshold", "Sets the threshold for the color cleanup"},
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
                case "-colorThreshold":
                    Console.WriteLine("Enter new color threshold: ");
                    var colInput = Console.ReadLine();
                    if (int.TryParse(colInput, out var colVal))
                    {
                        ColorThreshold = colVal;
                        Console.WriteLine("New Threshold: " + ColorThreshold);
                    }
                    else Console.WriteLine("--Invalid Input--");
                    break;
                case "-calibrate":
                    Calibration();
                    break;
                case "-run":
                    Console.WriteLine("Not yet implemented, use the debug command");
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
                        Threshold = value;
                        Console.WriteLine("New Threshold: " + Threshold);
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

        private static void Calibration()
        {
            while (Threshold == -1) { 
                Console.WriteLine("Enter Background Subtraction Threshold: ");
                Console.Write(": ");
                var thresholdInput = Console.ReadLine();
                if (int.TryParse(thresholdInput, out var value))
                {
                    Threshold = value;
                    Console.WriteLine("Threshold Set: " + Threshold);
                }
                else Console.WriteLine("--Invalid Input--");
            }

            while (ColorThreshold == -1)
            {
                Console.WriteLine("\nSet Color Cleaning Threshold: ");
                Console.Write(": ");
                var colInput = Console.ReadLine();
                if (int.TryParse(colInput, out var colVal))
                {
                    ColorThreshold = colVal;
                    Console.WriteLine("Threshold Set: " + ColorThreshold);
                }
                else Console.WriteLine("--Invalid Input--");
            }

            Console.WriteLine("Set File Input Name: ");
            Console.WriteLine("ATTENTION: Loading file needs to be located in the .EXE Root folder");
            Console.Write(": ");
            import = Console.ReadLine();
            Console.WriteLine("Import file set to: " + import);

            Console.WriteLine("Set Export File Name: ");
            Console.WriteLine("ATTENTION: File will be exported to the Export folder in the .EXE Root directory");
            Console.Write(": ");
            export = Console.ReadLine();
            Console.WriteLine("Export file set to: " + export);
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
            Program.StartProgram("debugBackground.bmp", "debugShapesOverlay.bmp", "debugOutput.bmp");
            stop.Stop();
            Console.WriteLine(" Debugging time: " + stop.ElapsedMilliseconds + " Milliseconds");
            Console.WriteLine("\nDebugging complete..");
        }
        
        public static int Threshold
        {
            get => _threshold;
            set => _threshold = value;
        }

        public static int ColorThreshold
        {
            get => _colorThreshold;
            set => _colorThreshold = value;
        }
    }
}