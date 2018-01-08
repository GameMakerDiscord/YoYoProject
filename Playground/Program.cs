using System;
using System.IO;
using YoYoProject;
using YoYoProject.Controllers;

namespace Playground
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var projectName = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            var rootDirectory = Path.Combine(@"C:\Temp\GMProjects", projectName);
            var project = GMProject.New();

            var config = project.Resources.Get<GMWindowsOptions>();
            
            config.DisplayName = "Hello, World!";

            var winConfig = project.Configs.Add("WindowsConfig", project.Configs.Default);
            project.Configs.SetConfig(winConfig);

            config.DisplayName = "Hello, World! WindowsConfig!!";
            Console.WriteLine("DisplayName = {0} [WindowsConfig]", config.DisplayName);

            project.Configs.SetConfig(project.Configs.Default);

            Console.WriteLine("DisplayName = {0} [default]", config.DisplayName);

            project.Save(rootDirectory);

            Console.WriteLine(projectName);
            Console.ReadKey();
        }
    }
}
