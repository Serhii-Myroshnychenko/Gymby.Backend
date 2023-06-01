using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.ApiTests.Utils
{
    public class FileBuilder
    {
        public static string GetPathToProfile(string file)
        {
            string basePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//..//"));
            string jsonFilePath = Path.Combine(basePath, "Data", "Profile", file);
            return jsonFilePath;
        }

        public static string GetPathToMeasurement(string file)
        {
            string basePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//..//"));
            string jsonFilePath = Path.Combine(basePath, "Data", "Measurement", file);
            return jsonFilePath;
        }

        public static string GetPathToFriend(string file)
        {
            string basePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//..//"));
            string jsonFilePath = Path.Combine(basePath, "Data", "Friend", file);
            return jsonFilePath;
        }
    }
}
