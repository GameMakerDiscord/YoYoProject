using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using YoYoProject.Models;

namespace YoYoProject
{
    public sealed class GMProject : ControllerBase<GMProject, GMProjectModel>
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
            Json.Serialize(path, Serialize());

            // ReSharper disable AssignNullToNotNullAttribute
            foreach (var resource in Resources)
            {
                var resourceInfo = (IGMResourceInfo)resource;

                // Base Resource
                var fullPath = Path.Combine(rootDirectory, resourceInfo.ResourcePath);
                var resourceDirectory = Path.GetDirectoryName(fullPath);
                FileSystem.EnsureDirectory(resourceDirectory);

                Json.Serialize(fullPath, resource.SerializeResource());

                // Config Deltas
                foreach (var config in resourceInfo.Configs)
                {
                    var configDirectory = Path.Combine(resourceDirectory, config.Name);
                    FileSystem.EnsureDirectory(configDirectory);

                    var configDeltaFilename = $"{Path.GetFileNameWithoutExtension(fullPath)}.{config.Name}.yy";
                    var fullConfigDeltaPath = Path.Combine(configDirectory, configDeltaFilename);

                    config.Serialize(fullConfigDeltaPath, resource.Id);
                }
            }
            // ReSharper restore AssignNullToNotNullAttribute
        }

        protected internal override void Deserialize(GMProjectModel model)
        {
            ParentProject = GMProjectParent.FromModel(model.parentProject);
        }

        protected internal override GMProjectModel Serialize()
        {
            return new GMProjectModel
            {
                parentProject = ParentProject.Serialize(),
                configs = Configs.Serialize(),
                resources = Resources.Serialize(),
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

            project.Resources.Create<GMWindowsOptions>();

            var testConfig = project.Configs.Add("Test", project.Configs.Root);
            var macOptions = project.Resources.Create<GMMacOptions>();
            macOptions.DisplayName = "DisplayName - default";
            macOptions.PushConfig(testConfig);
            macOptions.DisplayName = "DisplayName - Test";
            
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

    public sealed class GMProjectParent : ControllerBase<GMProjectParent, GMProjectParentModel>
    {
        protected internal override void Deserialize(GMProjectParentModel model)
        {
            
        }

        protected internal override GMProjectParentModel Serialize()
        {
            return new GMProjectParentModel
            {
                hiddenResources = new List<Guid>(),
                alteredResources = new Dictionary<Guid, GMResourceInfoModel>(),
                projectPath = "${base_project}"
            };
        }
    }

    // TODO IReadOnlyDictionary?
    public sealed class GMResourceManager : IEnumerable<IGMResource>
    {
        public int Count
        {
            get { return resources.Count; }
        }

        public IGMResource this[Guid key]
        {
            get { return resources[key]; }
        }

        private readonly SortedDictionary<Guid, IGMResource> resources;

        public GMResourceManager()
        {
            resources = new SortedDictionary<Guid, IGMResource>();
        }

        public TResource Create<TResource>()
            where TResource : IGMResource, new()
        {
            var resource = new TResource
            {
                Id = Guid.NewGuid()
            };

            resources.Add(resource.Id, resource);

            return resource;
        }

        public SortedDictionary<Guid, GMResourceInfoModel> Serialize()
        {
            var resourceInfos = new SortedDictionary<Guid, GMResourceInfoModel>();
            foreach (var kvp in resources)
            {
                var resourceInfo = (IGMResourceInfo)kvp.Value;
                resourceInfos.Add(kvp.Key, resourceInfo.Serialize());
            }

            return resourceInfos;
        }

        public IEnumerator<IGMResource> GetEnumerator()
        {
            return resources.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public interface IGMResource
    {
        Guid Id { get; set; }

        string Name { get; set; }

        ModelBase SerializeResource();
    }

    internal interface IGMResourceInfo
    {
        string ResourcePath { get; }

        IReadOnlyList<ConfigDelta> Configs { get; }

        GMResourceInfoModel Serialize();
    }

    public abstract class GMResource<TController, TModel> : ControllerBase<TController, TModel>, IGMResource, IGMResourceInfo
        where TController : GMResource<TController, TModel>, new()
        where TModel : ModelBase, new()
    {
        public string Name { get; set; }

        public bool Dirty { get; private set; }

        protected abstract string ResourcePath { get; }

        string IGMResourceInfo.ResourcePath => ResourcePath;

        IReadOnlyList<ConfigDelta> IGMResourceInfo.Configs => new List<ConfigDelta>(configDeltas.Values);

        private readonly Dictionary<string, ConfigDelta> configDeltas;
        private readonly Stack<ConfigDelta> configStack;

        protected GMResource()
        {
            Dirty = false;

            configDeltas = new Dictionary<string, ConfigDelta>();
            configStack = new Stack<ConfigDelta>();
        }

        protected T GetProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            foreach (var config in configStack)
            {
                object deltaValue;
                if (config.TryGetProperty(propertyName, out deltaValue))
                    return (T)deltaValue;
            }

            return value;
        }

        protected void SetProperty<T>(T value, out T storage, [CallerMemberName] string propertyName = null)
        {
            if (configStack.Count > 0)
            {
                var config = configStack.Peek();
                config.SetProperty(propertyName, value);
            }

            storage = value;
        }

        internal void PushConfig(ConfigTree.Node config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            string name = config.Name;

            ConfigDelta configDelta;
            if (!configDeltas.TryGetValue(name, out configDelta))
            {
                configDelta = new ConfigDelta(name);
                configDeltas.Add(name, configDelta);
            }

            configStack.Push(configDelta);
        }

        internal void PopConfig()
        {
            configStack.Pop();
        }

        // NOTE Required since C# doesn't allow explicit declaration of interfaces as abstract
        // TODO Since this is the case, that means that I can't directly access the specific model type for
        //      serialization and it's not even important since everything can be serialized from the base type
        //      maybe we can simply the pattern and not need to reference our model, which would fix some issues
        //      in the design
        ModelBase IGMResource.SerializeResource()
        {
            return Serialize();
        }

        GMResourceInfoModel IGMResourceInfo.Serialize()
        {
            return new GMResourceInfoModel
            {
                ResourcePath = ResourcePath,
                ResourceType = GetType().Name,

                // TODO Implement
                ResourceCreationConfigs = null,
                ConfigDeltas = configDeltas.Count == 0 ? null : configDeltas.Values.Select(x => x.Name).ToList(),
                ConfigDeltaFiles = null
            };
        }
    }

    internal sealed class ConfigDelta
    {
        private readonly static Version Version = new Version(1, 0, 0);
        private const char MetadataSep = '\u2190';
        private const char DataPairSep = '|';

        public string Name { get; }

        private readonly Dictionary<string, object> properties;

        public ConfigDelta(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            Name = name;
            properties = new Dictionary<string, object>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetProperty(string key, out object value)
        {
            return properties.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetProperty(string key, object value)
        {
            properties[key] = value;
        }

        public void Serialize(string path, Guid id)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var builder = new StringBuilder();
            builder.AppendFormat("{0}.{1}.{2}", Version.Major, Version.Minor, Version.Build);
            builder.Append(MetadataSep);
            builder.Append(id.ToString("D"));
            builder.Append(MetadataSep);

            builder.Append(id.ToString("D"));
            builder.Append(DataPairSep);
            builder.Append(Json.Serialize(properties));

            var contents = builder.ToString();

            File.WriteAllText(path, contents, Encoding.UTF8);
        }
    }

    public sealed class ConfigTree
    {
        public const string DefaultName = "default";

        public Node Root => nodes[DefaultName];

        public Dictionary<string, Node>.ValueCollection Configs => nodes.Values;

        private readonly Dictionary<string, Node> nodes;

        public ConfigTree()
        {
            nodes = new Dictionary<string, Node>();
            Add(DefaultName, null);
        }

        public Node Add(string name, Node parent)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            
            // TODO Validate name

            if (nodes.ContainsKey(name))
            {
                throw new InvalidOperationException(
                    $"Cannot add a new config with the name '{name}' because one already exists."
                );
            }

            var node = new Node(name, parent);
            parent?.Children.Add(node);

            nodes[name] = node;

            return node;
        }

        public Node Get(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return nodes[name];
        }

        public void Remove(string name)
        {
            throw new NotImplementedException();
        }

        internal List<string> Serialize()
        {
            var configs = Configs;
            var list = new List<string>(configs.Count);

            foreach (var config in configs)
            {
                var builder = new StringBuilder();
                var node = config;
                while (true)
                {
                    builder.Insert(0, node.Name);
                    if (node.Parent != null)
                    {
                        builder.Insert(0, ';');
                        node = node.Parent;
                    }
                    else
                        break;
                }

                list.Add(builder.ToString());
            }

            return list;
        }

        public sealed class Node
        {
            public string Name { get; set; }

            public Node Parent { get; }

            public List<Node> Children { get; }

            public Node(string name, Node parent)
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(name));

                if (parent == null && name != DefaultName)
                    throw new ArgumentNullException(nameof(parent));

                Name = name;
                Parent = parent;
                Children = new List<Node>();
            }
        }
    }

    public sealed class GMWindowsOptions : GMResource<GMWindowsOptions, GMWindowsOptionsModel>
    {
        public string DisplayName
        {
            get { return GetProperty(inner.option_windows_display_name); }
            set { SetProperty(value, out inner.option_windows_display_name); }
        }

        private readonly GMWindowsOptionsModel inner;

        protected override string ResourcePath
        {
            get { return @"options\main\options_main.yy"; }
        }

        public GMWindowsOptions()
        {
            inner = new GMWindowsOptionsModel();
        }

        protected internal override void Deserialize(GMWindowsOptionsModel model)
        {
            DisplayName = model.option_windows_display_name;
        }

        protected internal override GMWindowsOptionsModel Serialize()
        {
            return new GMWindowsOptionsModel
            {
                option_windows_display_name = DisplayName
            };
        }
    }

    public sealed class GMMacOptions : GMResource<GMMacOptions, GMMacOptionsModel>
    {
        public string DisplayName
        {
            get { return GetProperty(inner.option_mac_display_name, "option_mac_display_name"); }
            set { SetProperty(value, out inner.option_mac_display_name, "option_mac_display_name"); }
        }

        protected override string ResourcePath => @"options\mac\options_mac.yy";

        private readonly GMMacOptionsModel inner;

        public GMMacOptions()
        {
            inner = new GMMacOptionsModel();
        }

        protected internal override void Deserialize(GMMacOptionsModel model)
        {
            Id = model.id;
            Name = model.name;
            DisplayName = model.option_mac_display_name;
        }

        protected internal override GMMacOptionsModel Serialize()
        {
            return new GMMacOptionsModel
            {
                id = Id,
                name = "macOS",
                option_mac_display_name = DisplayName
            };
        }
    }

    public sealed class GMFolder : GMResource<GMFolder, GMFolderModel>
    {
        public string FolderName
        {
            get { return GetProperty(inner.folderName); }
            set { SetProperty(value, out inner.folderName); }
        }

        public string LocalizedName
        {
            get { return GetProperty(inner.localisedFolderName); }
            set { SetProperty(value, out inner.localisedFolderName); }
        }

        public string FilterType
        {
            get { return GetProperty(inner.filterType); }
            set { SetProperty(value, out inner.filterType); }
        }

        public bool IsDefaultView
        {
            get { return GetProperty(inner.isDefaultView); }
            set { SetProperty(value, out inner.isDefaultView); }
        }

        public List<IGMResource> Children { get; set; } 

        protected override string ResourcePath
        {
            get { return $@"views\{Id}.yy"; }
        }

        private readonly GMFolderModel inner;

        public GMFolder()
        {
            Children = new List<IGMResource>();
            inner = new GMFolderModel();
        }

        protected internal override void Deserialize(GMFolderModel model)
        {
            Id = model.id;
            Name = model.id.ToString();
            FolderName = model.folderName;
            LocalizedName = model.localisedFolderName;
            FilterType = model.filterType;
            IsDefaultView = model.isDefaultView;
        }

        protected internal override GMFolderModel Serialize()
        {
            return new GMFolderModel
            {
                id = Id,
                name = Id.ToString(),
                folderName = FolderName,
                children = Children.Select(x => x.Id).ToList(),
                filterType = FilterType,
                isDefaultView = IsDefaultView,
                localisedFolderName = LocalizedName
            };
        }
    }
}
