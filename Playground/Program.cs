using System;
using System.IO;
using YoYoProject;

namespace Playground
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var projectName = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            var rootDirectory = Path.Combine(@"C:\Temp\GMProjects", projectName);
            var project = GMProject.New();
            project.Save(rootDirectory);

            Console.WriteLine(projectName);
            Console.ReadKey();
        }
    }
}
