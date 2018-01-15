using System;
using System.IO;
using System.Linq;
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
            var project = GMProject.New(rootDirectory);

            {
                var sprite = project.Resources.Create<GMSprite>();
                var frame = sprite.Frames.Create();
                frame.SetImage(@"C:\Users\zreedy\Pictures\Avatar.jpeg");
            }

            {
                var sprite = project.Resources.Create<GMSprite>();
                var frame = sprite.Frames.Create();
                frame.SetImage(@"C:\Users\zreedy\Pictures\wallpaper\Mzc2oWU.jpg");
            }

            project.Save();

            Console.WriteLine(projectName);
            Console.ReadKey();
        }
    }
}
