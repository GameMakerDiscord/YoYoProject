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
            //{
            //    var sprite = project.Resources.Create<GMSprite>();
            //    var frame = sprite.Frames.Create();
            //    frame.SetImage(@"C:\Temp\GMProjects\_Resources\tilesheet_complete.png");

            //    var tileSet = project.Resources.Create<GMTileSet>();
            //    tileSet.Sprite = sprite;

            //    {
            //        var animation = tileSet.Animations.Create(2);
            //        animation.Frames[0].Set(0, 0);
            //        animation.Frames[1].Set(0, 4);
            //    }

            //    {
            //        var animation = tileSet.Animations.Create(4);
            //        animation.Frames[0].Set(1, 1);
            //        animation.Frames[1].Set(2, 1);
            //        animation.Frames[2].Set(3, 1);
            //        animation.Frames[3].Set(4, 1);
            //    }

            //    {
            //        var autoTileSet = tileSet.AutoTileSets.Create(GMAutoTileSetType.Transitional);
            //        autoTileSet.Tiles[0].Set(0, 0);
            //        autoTileSet.Tiles[1].Set(1, 0);
            //        autoTileSet.Tiles[2].Set(2, 0);
            //        autoTileSet.Tiles[3].Set(4, 0);
            //    }
            //}

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
