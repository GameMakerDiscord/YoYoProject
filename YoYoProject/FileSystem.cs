using System;
using System.Diagnostics;
using System.IO;

namespace YoYoProject
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
    }
}
