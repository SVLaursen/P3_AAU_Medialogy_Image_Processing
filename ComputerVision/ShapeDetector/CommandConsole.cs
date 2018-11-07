using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShapeDetector
{
    public static class CommandConsole
    {
        private static List<Color> _colors = new List<Color>();
        private static bool running;
        
        private static List<Commands> _commands = new List<Commands>( new []
            {
                new Commands("-help","displays commands"),
                new Commands("-exit", "exits application"),
                new Commands("-run", "runs image detection"),
                new Commands("-colors", "set colors"),
                new Commands("-calibrate", "calibrate the camera and software"),
                new Commands("-clear", "clears the console") 
            });

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
                default:
                    Console.WriteLine("--Invalid input--");
                    break;
            }
        }

        private static void SetColors()
        {
            var setting = true;

            while (setting)
            {
                var input = Console.ReadLine();
                
                if (input == "-exit" || input == "-back")
                {
                    setting = false;
                }
                else
                {
                    Console.WriteLine("--Invalid input--");
                }
            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("Below you'll find a list of commands and what they do:");
            
            foreach (var t in _commands)
            {
                Console.WriteLine(t.Command + " -- " + t.Description);
            }
        }
    }

    public struct Commands
    {
        private readonly string command;
        private readonly string description;

        public Commands(string command, string description)
        {
            this.command = command;
            this.description = description;
        }
        
        public string Command => command;
        public string Description => description;
    }
}