using System;
using System.Threading;

namespace BBB.NET.Base
{
    public class CommandConsole
    {
        const string prompt = "> ";

        string currentText = "";

        Thread thread;

        public CommandConsole()
        {
            thread = new Thread(() =>
            {
                while (true)
                {
                    Console.Write(prompt);
                    bool commandExecuted = false;
                    currentText = "";
                    while (!commandExecuted)
                    {
                        ConsoleKeyInfo key = Console.ReadKey();

                        switch (key.Key)
                        {
                            case ConsoleKey.Enter:
                                commandExecuted = true;
                                Console.WriteLine();

                                string[] argv = currentText.Split(' ');

                                switch (argv[0].ToLower())
                                {
                                    case "exit":
                                        Program.client.StopAsync().Wait();
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine($"Unrecognized command: \"{currentText}\"");
                                        break;
                                }

                                break;
                            case ConsoleKey.Backspace:
                                if (Console.CursorLeft >= prompt.Length)
                                {
                                    Console.Write(' ');
                                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                }
                                else
                                {
                                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                                }

                                if (currentText.Length > 0)
                                {
                                    currentText = currentText.Substring(0, currentText.Length - 1);
                                }
                                break;
                            case ConsoleKey.LeftArrow:
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                break;
                            case ConsoleKey.RightArrow:
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                break;
                            default:
                                currentText += key.KeyChar;
                                break;

                        }
                    }
                }
            });

            thread.Start();
        }

        public void PrintString(string val)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine(val);
            Console.Write(prompt + currentText);
        }
    }
}
