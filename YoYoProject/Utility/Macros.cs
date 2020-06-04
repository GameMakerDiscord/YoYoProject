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

        // nkrapivin: I may be wrong about these though, feel free to edit this section.

        private static string LatestRunnerPath => Path.Combine(
            LatestRuntimeDirectory,
            "windows",
            "Runner.exe"
        );

        private static string LatestIgorPath => Path.Combine(
            LatestRuntimeDirectory,
            "bin",
            "Igor.exe"
        );

        private static string LatestAssetCompilerPath => Path.Combine(
            LatestRuntimeDirectory,
            "bin",
            "GMAssetCompiler.exe"
        );

        private static string LatestWebserverPath => Path.Combine(
            LatestRuntimeDirectory,
            "bin",
            "GMWebServer.exe"
        );

        private static string LatestCompatibilityLibPath => Path.Combine(
            LatestRuntimeDirectory,
            "lib",
            "compatibility.zip"
        );

        private static string LatestHtml5RunnerPath => Path.Combine(
            LatestRuntimeDirectory,
            "html5",
            "scripts.html5.zip"
        );

        public static string Expand(string value)
        {
            string ret = value;

            if (ret.IndexOf('$') < 0)
                return ret;

            if (value.Contains("base_project"))
                ret = ret.Replace("${base_project}", LatestBaseProjectDirectory);
            if (value.Contains("runner_path"))
                ret = ret.Replace("${runner_path}", LatestRunnerPath);
            if (value.Contains("igor_path"))
                ret = ret.Replace("${igor_path}", LatestIgorPath);
            if (value.Contains("asset_compiler_path"))
                ret = ret.Replace("${asset_compiler_path}", LatestAssetCompilerPath);
            if (value.Contains("webserver_path"))
                ret = ret.Replace("${webserver_path}", LatestWebserverPath);
            if (value.Contains("lib_compatibility_path"))
                ret = ret.Replace("${lib_compatibility_path}", LatestCompatibilityLibPath);
            if (value.Contains("html5_runner_path"))
                ret = ret.Replace("${html5_runner_path}", LatestHtml5RunnerPath);

            return ret;
        }
    }
}
