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

            /*** Tilesets ***/
            {
                var sprite = project.Resources.Create<GMSprite>();
                var frame = sprite.Frames.Create();
                frame.SetImage(@"C:\Temp\GMProjects\_Resources\tilesheet_complete.png");

                var tileSet = project.Resources.Create<GMTileSet>();
                tileSet.Sprite = sprite;
            }

            /*** Sounds ***/
            //{
            //    var sound = project.Resources.Create<GMSound>();
            //    sound.SetAudioFile(@"C:\Temp\GMProjects\_Resources\nature.ogg");
            //}

            /*** Sprites ***/
            //{
            //    var sprite = project.Resources.Create<GMSprite>();
            //    sprite.Layers.Create();

            //    var frame = sprite.Frames.Create();
            //    frame.Layers[0].SetImage(@"C:\Temp\GMProjects\_Resources\01.png");
            //    frame.Layers[1].SetImage(@"C:\Temp\GMProjects\_Resources\00.png");

            //    var frame2 = sprite.Frames.Create();
            //    frame2.SetImage(@"C:\Temp\GMProjects\_Resources\10.png");
            //}

            project.Save();

            Console.WriteLine(projectName);
            Console.ReadKey();
        }
    }
}
