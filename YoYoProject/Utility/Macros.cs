using System;
using System.CodeDom;
using System.IO;
using System.Linq;

namespace YoYoProject.Utility
{
    internal static class Macros
    {
        private static string RuntimesDirectory => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "GameMakerStudio2",
            "Cache",
            "runtimes"
        );

        private static string LatestRuntimeDirectory
        {
            get
            {
                var directoryInfo = new DirectoryInfo(RuntimesDirectory);
                var latestRuntime = directoryInfo.GetDirectories().OrderBy(x => x.Name).LastOrDefault();
                if (latestRuntime == null)
                    return null;

                return Path.Combine(RuntimesDirectory, latestRuntime.Name);
            }
        }

        private static string LatestBaseProjectDirectory => Path.Combine(
            LatestRuntimeDirectory,
            "BaseProject"
        );

        public static string Expand(string value)
        {
            if (value.IndexOf('$') < 0)
                return value;

            return value.Replace("${base_project}", LatestBaseProjectDirectory);
        }
    }
}
