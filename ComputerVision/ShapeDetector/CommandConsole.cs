using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShapeDetector
{
    public static class CommandConsole
    {
        private static List<Color> _colors = new List<Color>();
        private static bool running;
        
        private static Dictionary<string, string> _command = new Dictionary<string, string>()
        {
            {"-help", "displays all commands"},
            {"-clear", "clears the console"},
            {"-exit", "exit the program"},
            {"-run", "runs the shape detection software"},
            {"-colors", "set the colors for the detection software"},
            {"-calibrate", "calibrates the software"}
        };

        public static void Run()
        {
            Console.WriteLine("Command Console up and running, please write your input.");
            Console.WriteLine("For list of commands write '-help'.");
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
                    break;
                case "-colors":
                    SetColors();
                    break;
                case "-calibrate":
                    Calibration();
                    break;
                case "-run":
                    Program.StartProgram();
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
            Console.WriteLine("Below you'll find a list of commands and what they do:");

            foreach (var t in _command)
            {
                Console.WriteLine(t.Key + " -- " + t.Value);
            }
            
            Console.WriteLine(" ");
            Console.WriteLine("-- When setting the color in the color settings mode remember that");
            Console.WriteLine("you choose what color you want to set by writing '-color_#'.");
            Console.WriteLine("There are 6 colors to be set in total --");
        }
    }
}