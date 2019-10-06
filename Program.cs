#region References
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace MEE6_Level_Up_Bot
{
    class EzConsole
    {
        /* This is a class I made just to make life a bit easier when it comes to console design */
        /* Does everything you would usually do line by line in one task */

        public void WriteLine(object value, ConsoleColor Color, bool read)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
            if (read) 
                Console.ReadLine();
        }

        public void Write(object value, ConsoleColor Color, bool read)
        {
            Console.ForegroundColor = Color;
            Console.Write(value);
            Console.ForegroundColor = ConsoleColor.White;
            if (read)
                Console.ReadLine();
        }
    }

    class Program
    {
        static List<string> Dictionary = new List<string>();
        static int Messages = 0; // Leave this at 0
        static int Cooldown = 61; // MEE6 grants EXP for messages sent with a 60 second interval, this can be changed for other level up bots
        // I set the cooldown to 61 just to be on the safe side.

        static void Main(string[] args)
        {
            EzConsole EzC = new EzConsole(); // Creates new variable to access EzConsole methods
            Console.Title = $"MEE6 Level-Up Bot | Developed by Centos#1337 | Messages: 0 | Cooldown: {Cooldown}"; // Messages will stay as 0 for now

            WriteTitle(); // Writes the big "MEE6" title


            if (args.Length <= 0) // If no file is provided... program ends
            {
                EzC.WriteLine(" [X] Please drag your wordlist onto this file (.txt only)!", ConsoleColor.Red, true); 
                // Users need to drag their .txt file onto the exe to open it, program stops
            }

            // If file is provided, program carries on and starts to read the file

            string ext = Path.GetExtension(args[0]).Replace(".", ""); // Gets the extension of input file

            if (ext != "txt") // If the file is not a txt file...
                EzC.WriteLine(" [X] Please only input .txt files containing your wordlist!", ConsoleColor.Red, true);

            // File is inputted and is a .txt file, carrying on...

            foreach(string line in File.ReadAllLines(args[0])) // for each line in the wordlist
            {
                // Populate the dictionary with the inputted word list
                Dictionary.Add(line);
            }

            // Dictionary now contains the words from the wordlist

            Task.Run(() => LoopTimer()); // Create a new thread to run the loop task

            Console.ReadLine(); // The end :D
        }

        static void SendMessage()
        {
            EzConsole EzC = new EzConsole();

            int index = new Random().Next(Dictionary.Count); // Selects a random string from the dictionary

            SendKeys.SendWait(Dictionary[index]); // Types out the string into the Discord message box
            SendKeys.SendWait("{ENTER}"); // Hits enter for you and sends the message

            Messages++; // Increments the message value

            EzC.WriteLine($" [+] Sent message: {Dictionary[index]}", ConsoleColor.Magenta, false); // Outputs to console

            Console.Title = $"MEE6 Level-Up Bot | Developed by Centos#1337 | Messages: {Messages} | Cooldown: {Cooldown}"; // Updates the console title
        }

        static void LoopTimer()
        {
            while (true)
            {
                Thread.Sleep(100); // Waits for 1 second
                if (Cooldown >= 1) // If the cooldown is bigger than or equal to 1 second
                {
                    Cooldown--; // Counts down the cooldown by 1
                    Console.Title = $"MEE6 Level-Up Bot | Developed by Centos#1337 | Messages: {Messages} | Cooldown: {Cooldown}";
                }
                else // If the cooldown is at 0
                {
                    SendMessage(); // Sends the message
                    Cooldown = 60; // Resets the cooldown
                    Console.Title = $"MEE6 Level-Up Bot | Developed by Centos#1337 | Messages: {Messages} | Cooldown: {Cooldown}"; // Updates the console title
                }
            }
        }

        static void WriteTitle()
        {
            /*
             * Writes the title to the console
            */
            EzConsole EzC = new EzConsole();
            EzC.WriteLine(" ███╗   ███╗███████╗███████╗ ██████╗ ", ConsoleColor.Magenta, false);
            EzC.WriteLine(" ████╗ ████║██╔════╝██╔════╝██╔════╝ ", ConsoleColor.Magenta, false);
            EzC.WriteLine(" ██╔████╔██║█████╗  █████╗  ███████╗ ", ConsoleColor.Magenta, false);
            EzC.WriteLine(" ██║╚██╔╝██║██╔══╝  ██╔══╝  ██╔═══██╗", ConsoleColor.Magenta, false);
            EzC.WriteLine(" ██║ ╚═╝ ██║███████╗███████╗╚██████╔╝", ConsoleColor.Magenta, false);
            EzC.WriteLine(" ╚═╝     ╚═╝╚══════╝╚══════╝ ╚═════╝ ", ConsoleColor.Magenta, false);
            EzC.WriteLine("                                     ", ConsoleColor.Magenta, false);
        }
    }
}
