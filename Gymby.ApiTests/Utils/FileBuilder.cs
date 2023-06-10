﻿namespace Gymby.ApiTests.Utils
{
    public class FileBuilder
    {
        public static string GetFilePath(string folder, string file)
        {
            string basePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//..//"));
            string jsonFilePath = Path.Combine(basePath, "Data", folder, file);
            return jsonFilePath;
        }
    }
}
