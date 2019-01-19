using System.IO;
using System.Reflection;

namespace GameManager.Env.Extentions
{
    public class PathExtension
    {
        public static string GetPath(int nbToRemove)
        {
            var strPath = Assembly.GetExecutingAssembly().Location;
            var splited = strPath.Split(Path.DirectorySeparatorChar);
            var str = "";
            for (var i = 0; i < splited.Length - nbToRemove; i++)
            {
                str = Path.Combine(str, splited[i]);
            }

            return str;
        }



        public static string HomeBot()
        {
            return GetPath(2);
        }

        public static string RootBot()
        {
            return GetPath(3);
        }
    }
}