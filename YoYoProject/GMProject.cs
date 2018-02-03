using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YoYoProject.Controllers;
using YoYoProject.Models;
using YoYoProject.Utility;

namespace YoYoProject
{
    public sealed class GMProject : ControllerBase
    {
        public string RootDirectory { get; private set; }

        public GMProjectParent ParentProject { get; private set; }

        public GMResourceManager Resources { get; private set; } 

        public ConfigTree Configs { get; private set; }

        public bool DragAndDrop { get; set; }

        public bool JavaScript { get; set; }

        public void Save()
        {
            var projectName = RootDirectory.GetTerminalDirectoryName();
            var path = Path.Combine(RootDirectory, projectName + ".yyp");

            FileSystem.EnsureDirectory(RootDirectory);
            Json.SerializeToFile(path, Serialize());

            // ReSharper disable AssignNullToNotNullAttribute
            var active = Configs.Active;

            foreach (var resource in Resources)
            {
                if (!resource.Dirty)
                    continue;

                // Base Resource
                var fullPath = Path.Combine(RootDirectory, resource.ResourcePath);
                var resourceDirectory = Path.GetDirectoryName(fullPath);
                FileSystem.EnsureDirectory(resourceDirectory);

                Configs.Active = Configs.Default;
                Json.SerializeToFile(fullPath, resource.Serialize());

                // Config Deltas
                foreach (var config in Configs.GetForResource(resource.Id))
                {
                    var configDirectory = Path.Combine(resourceDirectory, config.Name);
                    FileSystem.EnsureDirectory(configDirectory);

                    var configDeltaFilename = $"{Path.GetFileNameWithoutExtension(fullPath)}.{config.Name}.yy";
                    var fullConfigDeltaPath = Path.Combine(configDirectory, configDeltaFilename);
                    
                    ConfigDelta.SerializeToFile(fullConfigDeltaPath, resource, config);
                }
            }

            Configs.Active = active;
            // ReSharper restore AssignNullToNotNullAttribute
        }

        internal override ModelBase Serialize()
        {
            return new GMProjectModel
            {
                // TODO Would be nice if I knew the type here explicitly
                id = Id,
                parentProject = (GMProjectParentModel)ParentProject.Serialize(),
                configs = Configs.Serialize(),
                resources = Resources.Serialize(),
                IsDnDProject = DragAndDrop,
                option_ecma = JavaScript,
                script_order = Resources.GetAllOfType<GMScript>().Select(x => x.Id).ToList(),
                tutorial = ""
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            var project = (GMProjectModel)model;

            Id = project.id;
            DragAndDrop = project.IsDnDProject;
            JavaScript = project.option_ecma;

            Resources.Deserialize(project.resources);
        }

        public static GMProject Load(string rootDirectory)
        {
            if (rootDirectory == null)
                throw new ArgumentNullException(nameof(rootDirectory));

            rootDirectory = Macros.Expand(rootDirectory);
            var project = new GMProject
            {
                RootDirectory = rootDirectory,
                ParentProject = new GMProjectParent(),
                Resources = null,
                Configs = new ConfigTree(),
            };

            project.Resources = new GMResourceManager(project); // TODO Ewww

            var projectName = rootDirectory.GetTerminalDirectoryName();
            var path = Path.Combine(rootDirectory, projectName + ".yyp");
            var model = Json.Deserialize<GMProjectModel>(path);

            project.Deserialize(model);

            return project;
        }

        public static GMProject New(string rootDirectory)
        {
            if (rootDirectory == null)
                throw new ArgumentNullException(nameof(rootDirectory));

            var project = new GMProject
            {
                Id = Guid.NewGuid(),
                RootDirectory = rootDirectory,
                ParentProject = new GMProjectParent(),
                Resources = null,
                Configs = new ConfigTree(),
                DragAndDrop = false,
                JavaScript = false
            };

            project.Resources = new GMResourceManager(project); // TODO Ewww

            // TODO Inherit from BaseProject
            project.Resources.Create<GMMainOptions>();
            project.Resources.Create<GMWindowsOptions>();
            project.Resources.Create<GMMacOptions>();
            project.Resources.Create<GMLinuxOptions>();
            
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
