using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace ShapeDetector
{
    public static class CommandConsole
    {
        private static bool running;
        private const string versionNumber = "0.5 DEBUG";
        private static string import = "N/A";
        private static string export = "N/A";
        
        public static int Threshold { get; set; } = -1;
        public static int ColorThreshold { get; set; } = -1;

        private static readonly Dictionary<string, string> _command = new Dictionary<string, string>
        {
            {"-help", "Displays all commands"},
            {"-clear", "Clears the console"},
            {"-calibrate", "Calibrates the software"},
            {"-run", "Runs the shape detection software"},
            {"-colorThreshold", "Sets the threshold for the color cleanup"},
            {"-import", "Sets the image import path"},
            {"-export", "Sets the file export name."},
            {"-threshold", "Sets the color threshold value"},
            {"-background", "Set the background image for substraction" },
            {"-debug", "Runs system with default debugging values"},
            {"-reset", "Resets system to empty values"},
            {"-exit", "Exit the program"},
        };

        public static void Run()
        {
            if (File.Exists("savestate.ini")) Load();
            else File.Create("savestate.ini");
            
            Load();
            Console.Clear();
            Console.WriteLine("MTA-18336 Image Processing Software; Version " + versionNumber);
            
            Console.WriteLine("\nCurrent Settings: " + "\nThreshold: " + Threshold);
            Console.WriteLine("Import File: " + import + "\nExport File: " + export + "\n");
            
            Console.WriteLine("For list of commands write '-help'.");
            if(Threshold <= 0 || ColorThreshold <= 0)
            {
                Console.WriteLine("\nATTENTION: System not calibrated. Calibration is required for system to run as intended.");
                Console.WriteLine("To calibrate, run process: -calibrate");
            } 
            
            running = true;
            while (running) DetectInput(Console.ReadLine());
        }

        private static void DetectInput(string input)
        {
            switch (input)
            {
                case "-help":
                    DisplayHelp();
                    break;
                
                case "-exit":
                    Save();
                    running = false;
                    break;
                
                case "-clear":
                    Console.Clear();
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
                    Save();
                    break;
                
                case "-calibrate":
                    Calibration();
                    break;
                
                case "-run":
                    Console.WriteLine("Not yet implemented, use the debug command");
                    RunDetection();
                    break;
                
                case "-import":
                    Console.WriteLine("Set image import file::");
                    import = Console.ReadLine();
                    Console.WriteLine("New image import file: " + import);
                    Save();
                    break;
                
                case "-export":
                    Console.WriteLine("Set image export file:");
                    export = Console.ReadLine();
                    Console.WriteLine("New export file: " + export);
                    Save();
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
                    Save();
                    break;
                
                case  "-background":
                    Console.WriteLine("Set background: ");
                    SetBackground();
                    break;
                
                case "-debug":
                    Debug();
                    break;
                
                case "-reset":
                    Reset();
                    break;
                
                default:
                    Console.WriteLine("--Invalid input--");
                    break;
            }
        }

        private static void Calibration()
        {
            while (Threshold <= 0) { 
                Console.WriteLine("\nEnter Background Subtraction Threshold: ");
                Threshold = SetThreshold(Console.ReadLine());
            }

            while (ColorThreshold <= 0)
            {
                Console.WriteLine("\nEnter Color Cleaning Threshold: ");
                ColorThreshold = SetThreshold(Console.ReadLine());
            }
            
            Console.WriteLine("Set background: ");
            SetBackground();

            Console.WriteLine("\nSet Input File Name: ");
            Console.WriteLine("ATTENTION: Loading file needs to be located in the .EXE Root folder");
            import = Console.ReadLine();
            Console.WriteLine("Import file set to: " + import);

            Console.WriteLine("\nSet Export File Name: ");
            Console.WriteLine("ATTENTION: File will be exported to the Export folder in the .EXE Root directory");
            export = Console.ReadLine();
            Console.WriteLine("Export file set to: " + export +"\n--Calibration Done--");
            Save();
            

            int SetThreshold(string consoleInput)
            {
                if (int.TryParse(consoleInput, out var value))
                {
                    Console.WriteLine("Threshold set: " + value);
                    return value;
                }
                
                Console.WriteLine("--Invalid Input--");
                return 0;
            }
        }

        private static void SetBackground()
        {
            Console.WriteLine("Press any key to take a picture");
            var input = Console.ReadLine();
            //  Bitmap image = ImageHandler.CaptureImage();

            //  if (File.Exists("Background.bmp"))
            //{
            //  File.Delete("Background.bmp");
            //}

            //image.Save("Background.bmp");
            Program.backgroundImage = ImageHandler.CaptureImage();
            Console.WriteLine("Background Set");
            
        }
        private static void DisplayHelp()
        {
            Console.WriteLine("List of Commands: ");

            foreach (var t in _command)
            {
                Console.WriteLine("' " + t.Key+ " '" + " --> " + t.Value);
            }
        }

        private static void Debug()
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
            Console.WriteLine("Running System Debug...");
            Program.StartProgram("debugBackground.bmp", "debugShapes.bmp", "debugOutput.bmp");
            stop.Stop();
            Console.WriteLine(" Debugging time: " + stop.ElapsedMilliseconds + " Milliseconds");
            Console.WriteLine("\nDebugging complete..");
        }
        
        private static void RunDetection()
        {
            if (Program.backgroundImage != null)
            {
                Stopwatch stop = new Stopwatch();
                stop.Start();
                Console.WriteLine("Running Detection...");
                // Bitmap shapes = ImageHandler.CaptureImage();
                // 
                // if (File.Exists("Shapes.bmp"))
                //{
                //   File.Delete("Shapes.bmp");
                //}
                //shapes.Save("Shapes.bmp");
                Program.shapeImage = ImageHandler.CaptureImage();
                Program.StartProgram(Program.backgroundImage, Program.shapeImage, "Output.bmp");

                stop.Stop();
                Console.WriteLine(" Run Time: " + stop.ElapsedMilliseconds + " Milliseconds");
                Console.WriteLine("\n Detection Complete!");
            }
            else
            {
                Console.WriteLine("ERROR: Background image not set!");                    
            }
        }


        private static void Save()
        {
            TextWriter tw = new StreamWriter("savestate.ini");
            tw.WriteLine(Threshold);
            tw.WriteLine(ColorThreshold);
            tw.WriteLine(import);
            tw.WriteLine(export);
            tw.Close();
        }
        private static void Load()
        {
            TextReader tr = new StreamReader("savestate.ini");
            Threshold = Convert.ToInt32(tr.ReadLine());
            ColorThreshold = Convert.ToInt32(tr.ReadLine());
            import = tr.ReadLine();
            export = tr.ReadLine();
            tr.Close();
        }
        private static void Reset()
        {
            if (File.Exists("savestate.ini"))
            {
                File.Delete("savestate.ini");
                File.Create("savestate.ini");
            }
            Console.WriteLine("Savestate reset!");
        }
    }
}