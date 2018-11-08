using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShapeDetector
{
    public static class CommandConsole
    {
        private static List<Color> _colors = new List<Color>();
        private static bool running;
        private static string versionNumber = "0.1";
        private static int threshold = 50;
        private static String import = "image.png";
        private static String export = "image.bmp";
        
        private static Dictionary<string, string> _command = new Dictionary<string, string>()
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
            Header();
            CurrentSettings();
            Console.WriteLine("For list of commands write '-help'.");
            Console.Write(": ");
            running = true;
            
            while (running)
            {
                DetectInput(Console.ReadLine());
            }
        }

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
                    Program.StartProgram(_colors, threshold, import, export);
                    break;
                case "-import":
                    Import();
                    break;
                case "-export":

                    break;
                case "-threshold":

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
        //Let's hope we don't have to use and that we can automate as much as possible
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
                Console.WriteLine("COMPLETE - Returning to main menu");
                setting = false;

            }

            int SetVal(string input)
            {
                if (int.TryParse(input, out var value)) return value;
                
                Console.WriteLine("-- Invalid Input --");
                Console.WriteLine("Value automatically signed to 0");
                return 0;
            }
        }

        private static void Calibration()
        {
            //TODO: Calibration code get called from here.
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("List of Commands: ");

            foreach (var t in _command)
            {
                Console.WriteLine(t.Key + " -- " + t.Value);
            }
            
            Console.WriteLine(" ");
            Console.WriteLine("-- When setting the color in the color settings mode remember that");
            Console.WriteLine("you choose what color you want to set by writing '-color_#'.");
            Console.WriteLine("There are 6 colors to be set in total --");
        }
        public static void Header()
        {
            Console.WriteLine("MTA-18336 Image Processing Software; Version " + versionNumber);
        }
        public static void CurrentSettings()
        {
            Console.WriteLine("");
            Console.WriteLine("Current Settings: ");
            Console.WriteLine("Threshold: " + threshold);
            Console.WriteLine("Import Path: " + import);
            Console.WriteLine("Export File Name: " + export);
            Console.WriteLine("");
        }
        public static void Import()
        {
            Console.Write("Set image import path: ");
            import = Console.ReadLine();
            Console.WriteLine("New image import path set to " + import);
            Console.Write(": ");
        }
        public static void Debug()
        {
            Console.WriteLine("Running System Debug...");

            //Add debug colors to the debug color list
            List<Color> _c = new List<Color>();
            _c.Add(Color.Red);
            _c.Add(Color.Blue);
            Color green = Color.FromArgb(000, 255, 000);
            _c.Add(green);

            Program.StartProgram(_c, 50, "test2.png", "debug");
            Console.WriteLine("Debugging complete..");
        }
    }
}