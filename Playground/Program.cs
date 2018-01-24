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
            var project = GMProject.New(rootDirectory);

            /*** Rooms ***/
            {
                var sPlayer = project.Resources.Create<GMSprite>();
                var frame = sPlayer.Frames.Create();
                frame.SetImage(@"C:\Temp\GMProjects\_Resources\00.png");

                var oPlayer = project.Resources.Create<GMObject>();
                oPlayer.Sprite = sPlayer;

                var room = project.Resources.Create<GMRoom>();
                room.CreationCode = "// Room Creation Code";

                var instLayer = room.Layers.Create<GMRInstanceLayer>("Instances");
                var instPlayer = instLayer.Instances.Create(oPlayer);
                instPlayer.CreationCode = $"// Instance Creation Code - {instPlayer.Name}";

                var bgLayer = room.Layers.Create<GMBackgroundLayer>("Background");
                bgLayer.Sprite = sPlayer;
                bgLayer.HTiled = true;
                bgLayer.VTiled = true;

                var aLayer = room.Layers.Create<GMRLayer>("foo");
                var bLayer = aLayer.Layers.Create<GMRInstanceLayer>("bar instances");
            }

            /*** Objects ***/
            //{
            //    var sprite = project.Resources.Create<GMSprite>();
            //    var frame = sprite.Frames.Create();
            //    frame.Layers[0].SetImage(@"C:\Temp\GMProjects\_Resources\01.png");

            //    var oParent = project.Resources.Create<GMObject>();
            //    oParent.Name = "oParent";
            //    oParent.Sprite = sprite;

            //    var createEvent = oParent.Events.Create(GMEventType.Create);
            //    createEvent.Contents = "/// Create Event";

            //    var stepEndEvent = oParent.Events.Create(GMEventType.Step, GMEventNumber.StepEnd);
            //    stepEndEvent.Contents = "/// Step End Event";

            //    oParent.Properties.Create(GMObjectPropertyType.Real, "a", "1234");
            //    oParent.Properties.Create(GMObjectPropertyType.String, "b", "\"Hello\"");

            //    var oChild = project.Resources.Create<GMObject>();
            //    oChild.Name = "oChild";
            //    oChild.Parent = oParent;

            //    var collisionEvent = oChild.Events.Create(GMEventType.Collision, oParent);
            //    collisionEvent.Contents = "/// oParent collision";

            //    oChild.Properties.Create(GMObjectPropertyType.Real, "a", "4321");
            //    oChild.Properties.Create(GMObjectPropertyType.String, "c", "\"World\"");
            //}

            /*** Timelines ***/
            //{
            //    var timeline = project.Resources.Create<GMTimeline>();
            //    var moment0 = timeline.Moments.Create(0);
            //    moment0.Contents = "/// Moment 0";

            //    var moment1234 = timeline.Moments.Create(1234);
            //    moment1234.Contents = "/// Moment 1234";
            //}

            /*** Fonts ***/
            //{
            //    var font = project.Resources.Create<GMFont>();
            //    font.FontName = "Consolas";
            //    font.SampleText += "WOW!";
            //}

            /*** Shaders ***/
            //{
            //    var shader = project.Resources.Create<GMShader>();
            //    shader.FragmentContents = "/* Fragment Shader */";
            //    shader.VertexContents = "/* Vertex Shader */";
            //}

            /*** Scripts ***/
            //{
            //    var script = project.Resources.Create<GMScript>();
            //    script.Contents = $@"show_debug_message(""{script.Name}"");";
            //}

            /*** Pathes ***/
            //{
            //    var path = project.Resources.Create<GMPath>();
            //    path.Closed = true;
            //    path.Kind = GMPathKind.SmoothCurves;
            //    path.Points.Create(188, 153);
            //    path.Points.Create(204, 231);
            //    path.Points.Create(365, 180);
            //    path.Points.Create(312, 92);
            //}

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
