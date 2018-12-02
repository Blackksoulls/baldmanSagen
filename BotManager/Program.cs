using System;
using System.Diagnostics;

namespace BotManager
{
    class Program
    {
        private static void Main(string[] args)
        {
            var p = new ProcessStartInfo("cmd", "dir");
            p.WorkingDirectory = "../../../../";
            Process.Start(p);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
