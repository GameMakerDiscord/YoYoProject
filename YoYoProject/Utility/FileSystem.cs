using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace YoYoProject.Utility
{
    [DebuggerStepThrough]
    internal static class FileSystem
    {
        public static void EnsureDirectory(string directory)
        {
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public static string GetTerminalDirectoryName(this string fullPath)
        {
            return fullPath?.Split(new[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
        }
    }
}
