using System;
using System.Diagnostics;
using System.Reflection;

namespace BBB.NET.Base
{
    enum LogType
    {
        Info,
        Warning,
        Error,
    }

    static class Logger
    {
        public static void Log(string message, LogType type = LogType.Info)
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(3);
            MethodBase method = frame.GetMethod();
            string methodName = method.Name;
            string className = method.DeclaringType.Name;

            string fullMessage = $"[{DateTime.Now.ToLongTimeString()} / {type.ToString()}] {message}";

            if (Program.commandConsole != null)
            {
                Program.commandConsole.PrintString(fullMessage);
                return;
            }

            Console.WriteLine(fullMessage);
        }
    }
}
