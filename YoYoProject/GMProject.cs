using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YoYoProject.Models;

namespace YoYoProject
{
    public sealed class GMProject : ControllerBase
    {
        public GMProjectParent ParentProject { get; private set; }

        public GMResourceManager Resources { get; private set; } 

        public ConfigTree Configs { get; private set; }

        public bool DragAndDrop { get; set; }

        public bool JavaScript { get; set; }

        public void Save(string rootDirectory)
        {
            if (rootDirectory == null)
                throw new ArgumentNullException(nameof(rootDirectory));

            var projectName = rootDirectory.Split(new[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
            var path = Path.Combine(rootDirectory, projectName + ".yyp");

            FileSystem.EnsureDirectory(rootDirectory);
            Json.SerializeToFile(path, Serialize());

            // ReSharper disable AssignNullToNotNullAttribute
            foreach (var resource in Resources)
            {
                // Base Resource
                var fullPath = Path.Combine(rootDirectory, resource.ResourcePath);
                var resourceDirectory = Path.GetDirectoryName(fullPath);
                FileSystem.EnsureDirectory(resourceDirectory);

                Json.SerializeToFile(fullPath, resource.Serialize());

                // Config Deltas
                foreach (var config in resource.Configs)
                {
                    var configDirectory = Path.Combine(resourceDirectory, config.Name);
                    FileSystem.EnsureDirectory(configDirectory);

                    var configDeltaFilename = $"{Path.GetFileNameWithoutExtension(fullPath)}.{config.Name}.yy";
                    var fullConfigDeltaPath = Path.Combine(configDirectory, configDeltaFilename);

                    config.SerializeToFile(fullConfigDeltaPath);
                }
            }
            // ReSharper restore AssignNullToNotNullAttribute
        }

        protected internal override ModelBase Serialize()
        {
            return new GMProjectModel
            {
                // TODO Would be nice if I knew the type here explicitly
                parentProject = (GMProjectParentModel)ParentProject.Serialize(),
                configs = Configs.Serialize(),
                resources = Resources.SerializeResourceInfo(),
                IsDnDProject = DragAndDrop,
                option_ecma = JavaScript,
                script_order = new List<Guid>(), // TODO Order script resources appear in Scripts GMFolder
                tutorial = ""
            };
        }

        public static GMProject New()
        {
            var project = new GMProject
            {
                ParentProject = new GMProjectParent(),
                Resources = new GMResourceManager(),
                Configs = new ConfigTree(),
                DragAndDrop = false,
                JavaScript = false
            };

            project.Resources.Create<GMWindowsOptions>(); // TODO Inherit from BaseProject
            project.Resources.Create<GMMacOptions>();
            
            var root = project.Resources.Create<GMFolder>();
            root.IsDefaultView = true;
            root.FolderName = "Default";
            root.FilterType = "root";

            root.Children.Add(project.AddResourceFolder("GMSprite", "sprites", "ResourceTree_Sprites"));
            root.Children.Add(project.AddResourceFolder("GMTileSet", "tilesets", "ResourceTree_Tilesets"));
            root.Children.Add(project.AddResourceFolder("GMSound", "sounds", "ResourceTree_Sounds"));
            root.Children.Add(project.AddResourceFolder("GMPath", "paths", "ResourceTree_Paths"));
            root.Children.Add(project.AddResourceFolder("GMScript", "scripts", "ResourceTree_Scripts"));
            root.Children.Add(project.AddResourceFolder("GMShader", "shaders", "ResourceTree_Shaders"));
            root.Children.Add(project.AddResourceFolder("GMFont", "fonts", "ResourceTree_Fonts"));
            root.Children.Add(project.AddResourceFolder("GMTimeline", "timelines", "ResourceTree_Timelines"));
            root.Children.Add(project.AddResourceFolder("GMObject", "objects", "ResourceTree_Objects"));
            root.Children.Add(project.AddResourceFolder("GMRoom", "rooms", "ResourceTree_Rooms"));
            root.Children.Add(project.AddResourceFolder("GMNotes", "notes", "ResourceTree_Notes"));
            root.Children.Add(project.AddResourceFolder("GMIncludedFile", "datafiles", "ResourceTree_IncludedFiles"));
            root.Children.Add(project.AddResourceFolder("GMExtension", "extensions", "ResourceTree_Extensions"));
            root.Children.Add(project.AddResourceFolder("GMOptions", "options", "ResourceTree_Options"));
            root.Children.Add(project.AddResourceFolder("GMConfig", "configs", "ResourceTree_Configs"));
            
            return project;
        }

        private GMFolder AddResourceFolder(string model, string folderName, string localizedName)
        {
            var folder = Resources.Create<GMFolder>();
            folder.FolderName = folderName;
            folder.LocalizedName = localizedName;
            folder.FilterType = model;

            return folder;
        }
    }
}
