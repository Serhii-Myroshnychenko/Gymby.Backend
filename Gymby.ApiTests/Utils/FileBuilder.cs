using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.ApiTests.Utils
{
    public class FileBuilder
    {
        public static string GetPathToJson(string jsonFile)
        {
            //return "D:\\Project\\Gymby.Backend\\Gymby.ApiTests\\Data\\Profile\\" + jsonFile;
            string basePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string jsonFilePath = Path.Combine(basePath, "Data", "Profile", jsonFile);
            return jsonFilePath;
        }
    }
}
