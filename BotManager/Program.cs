using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace BotManager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
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

            Process.Start("cmd.exe", $"/C dotnet {strGameBot}");
            Process.Start("cmd.exe", $"/C dotnet {strEventBot}");



            Console.WriteLine("All bots disconnected");
        }

        private static void Exited(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }
    }
}
