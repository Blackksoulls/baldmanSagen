using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace BotManager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            var strPath = Assembly.GetExecutingAssembly().Location;
            var strs = strPath.Split(Path.DirectorySeparatorChar);
            var str = "";
            for (var i = 0; i < strs.Length - 3; i++)
            {
                str = Path.Combine(str, strs[i]);
            }


            var gameInfo = new ProcessStartInfo("dotnet")
            {
                WorkingDirectory = Path.Combine(new[] {str , "GameManager", "Bin"}),
                FileName = "GameManager.dll"
            };

            var eventInfo = new ProcessStartInfo("dotnet")
            {
                WorkingDirectory = Path.Combine(new[] { str, "EventManager", "Bin" }) + Path.DirectorySeparatorChar,
                FileName = "EventManager.dll"
            };
     


            var strGameBot = Path.Combine(new[] { str, "GameManager", "Bin", "GameManager.dll" });
            var strEventBot = Path.Combine(new[] {str, "EventManager", "Bin", "EventManager.dll" });


            var p1 = Process.Start("cmd.exe", $"/C dotnet {strGameBot}");
            var p2 = Process.Start("cmd.exe", $"/C dotnet {strEventBot}");

            while (!p1.HasExited && !p2.HasExited)
            {
                p1.WaitForExit(1000);
                p2.WaitForExit(1000);
            }
            Console.WriteLine("All bots disconnected");
            Environment.Exit(0);

        }

        private static void Exited(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }
    }
}
