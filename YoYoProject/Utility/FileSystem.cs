using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace YoYoProject.Utility
{
    [DebuggerStepThrough]
    internal static class FileSystem
    {
        public static void EnsureDirectory(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static string GetTerminalDirectoryName(this string fullPath)
        {
            return fullPath?.Split(new[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
        }
    }
}
