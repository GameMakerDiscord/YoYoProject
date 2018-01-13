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

            var options = project.Resources.Get<GMMainOptions>();

            var sprite = project.Resources.Create<GMSprite>();
            sprite.TextureGroup = options.Graphics.DefaultTextureGroup;

            var frame = sprite.CreateFrame();
            frame.SetImage(@"C:\Users\zreedy\Pictures\Avatar.jpeg");

            project.Save();

            Console.WriteLine(projectName);
            Console.ReadKey();
        }
    }
}
