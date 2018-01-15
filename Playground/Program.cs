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
                sprite.Layers.Create();

                var frame = sprite.Frames.Create();
                frame.Layers[0].SetImage(@"C:\Temp\GMProjects\Empty\sprites\sprite0\layers\13c31a41-1e22-45cb-9c4b-6887010daa24\4645c3b9-1c75-45cc-833e-39cf10147ba2.png");
                frame.Layers[1].SetImage(@"C:\Temp\GMProjects\Empty\sprites\sprite0\layers\13c31a41-1e22-45cb-9c4b-6887010daa24\026bb964-04dc-432d-93f3-69142545b815.png");

                var frame2 = sprite.Frames.Create();
                frame2.SetImage(@"C:\Temp\GMProjects\Empty\sprites\sprite0\layers\527bd732-b55b-414d-af02-bb66b2e7f70a\026bb964-04dc-432d-93f3-69142545b815.png");
            }

            //{
            //    var sprite = project.Resources.Create<GMSprite>();
            //    var frame = sprite.Frames.Create();
            //    frame.SetImage(@"C:\Users\zreedy\Pictures\wallpaper\Mzc2oWU.jpg");
            //}

            project.Save();

            Console.WriteLine(projectName);
            Console.ReadKey();
        }
    }
}
