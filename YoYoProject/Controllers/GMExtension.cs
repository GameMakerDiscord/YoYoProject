using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YoYoProject.Common;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMExtension : GMResource
    {
        private BuildVersion version;
        public BuildVersion Version
        {
            get { return GetProperty(version); }
            set { SetProperty(value, ref version); }
        }
        
        private TargetPlatforms targetPlatforms;
        public TargetPlatforms TargetPlatforms
        {
            get { return GetProperty(targetPlatforms); }
            set { SetProperty(value, ref targetPlatforms); }
        }

        public GMExtensionIos iOS { get; }

        public GMExtensionAndroid Android { get; }

        public FileManager Files { get; }
        
        protected internal override string ResourcePath => $@"extensions\{Name}\{Name}.yy";

        public GMExtension()
        {
            iOS = new GMExtensionIos(this);
            Android = new GMExtensionAndroid();
            Files = new FileManager(this);
        }

        protected internal override void Create()
        {
            Name = Project.Resources.GenerateValidName("extension");
            Version = new BuildVersion(1, 0, 0);
            TargetPlatforms = TargetPlatforms.AllPlatforms;
        }

        internal override ModelBase Serialize()
        {
            return new GMExtensionModel
            {
                id = Id,
                name = Name,
                extensionName = "",
                version = Version.ToString(),
                packageID = "",
                productID = "",
                author = "",
                date = DateTime.Now,
                license = "",
                description = "",
                helpfile = "",
                androidProps = Android.Enabled,
                installdir = "",
                files = Files.Serialize(),
                classname = "",
                androidclassname = Android.ClassName,
                sourcedir = "",
                macsourcedir = "",
                maccompilerflags = iOS.CompilerFlags,
                maclinkerflags = iOS.LinkerFlags,
                iosplistinject = iOS.PlistInject,
                androidinject = Android.ApplicationInject,
                androidmanifestinject = Android.ManifestInject,
                gradleinject = Android.GradleInject,
                iosSystemFrameworkEntries = iOS.SystemFrameworks.Serialize(),
                iosThirdPartyFrameworkEntries = iOS.ThirdPartyFrameworks.Serialize(),
                IncludedResources = new List<string>(),
                androidPermissions = Android.Permissions,
                copyToTargets = TargetPlatforms
            };
        }

        public sealed class GMExtensionIos : ControllerBase
        {
            private bool enabled;
            public bool Enabled
            {
                get { return GetProperty(enabled); }
                set { SetProperty(value, ref enabled); }
            }

            private string compilerFlags;
            public string CompilerFlags
            {
                get { return GetProperty(compilerFlags); }
                set { SetProperty(value, ref compilerFlags); }
            }
            
            private string linkerFlags;
            public string LinkerFlags
            {
                get { return GetProperty(linkerFlags); }
                set { SetProperty(value, ref linkerFlags); }
            }
            
            private string className;
            public string ClassName
            {
                get { return GetProperty(className); }
                set { SetProperty(value, ref className); }
            }
            
            private string plistInject;
            public string PlistInject
            {
                get { return GetProperty(plistInject); }
                set { SetProperty(value, ref plistInject); }
            }
            
            public FrameworkManager SystemFrameworks { get; }

            public FrameworkManager ThirdPartyFrameworks { get; }
            
            internal GMExtensionIos(GMExtension extension)
            {
                SystemFrameworks = new FrameworkManager(extension);
                ThirdPartyFrameworks = new FrameworkManager(extension);
            }

            internal override ModelBase Serialize()
            {
                throw new InvalidOperationException();
            }
        }

        public sealed class GMExtensionAndroid : ControllerBase
        {
            private bool enabled;
            public bool Enabled
            {
                get { return GetProperty(enabled); }
                set { SetProperty(value, ref enabled); }
            }

            private string className;
            public string ClassName
            {
                get { return GetProperty(className); }
                set { SetProperty(value, ref className); }
            }
            
            private List<string> permissions;
            public List<string> Permissions
            {
                get { return GetProperty(permissions); }
                set { SetProperty(value, ref permissions); }
            }
            
            private string gradleInject;
            public string GradleInject
            {
                get { return GetProperty(gradleInject); }
                set { SetProperty(value, ref gradleInject); }
            }
            
            private string manifestInject;
            public string ManifestInject
            {
                get { return GetProperty(manifestInject); }
                set { SetProperty(value, ref manifestInject); }
            }
            
            private string applicationInject;
            public string ApplicationInject
            {
                get { return GetProperty(applicationInject); }
                set { SetProperty(value, ref applicationInject); }
            }
            
            private string runnerActivityInject;
            public string RunnerActivityInject
            {
                get { return GetProperty(runnerActivityInject); }
                set { SetProperty(value, ref runnerActivityInject); }
            }
            
            internal GMExtensionAndroid()
            {
                
            }

            internal override ModelBase Serialize()
            {
                throw new InvalidOperationException();
            }
        }

        public sealed class FileManager : IReadOnlyList<GMExtensionFile>
        {
            public int Count => files.Count;

            public GMExtensionFile this[int index] => files[index];

            private readonly List<GMExtensionFile> files;
            private readonly GMExtension extension;

            internal FileManager(GMExtension extension)
            {
                if (extension == null)
                    throw new ArgumentNullException(nameof(extension));

                files = new List<GMExtensionFile>();

                this.extension = extension;
            }

            public GMExtensionFile CreateFromDisk(string path)
            {
                var file = new GMExtensionFile(extension)
                {
                    Project = extension.Project,
                    Id = Guid.NewGuid()
                };

                file.SetFile(path);

                files.Add(file);

                return file;
            }

            public GMExtensionFile CreateNew(string filename)
            {
                var file = new GMExtensionFile(extension)
                {
                    Project = extension.Project,
                    Id = Guid.NewGuid()
                };

                file.CreateNewFile(filename);

                files.Add(file);

                return file;
            }

            public GMExtensionFile Get(string filename)
            {
                return files.FirstOrDefault(x => x.Filename == filename);
            }

            public void Delete(GMExtensionFile file)
            {
                files.Remove(file);
            }

            internal List<GMExtensionFileModel> Serialize()
            {
                return files.Select(x => (GMExtensionFileModel)x.Serialize()).ToList();
            }

            public IEnumerator<GMExtensionFile> GetEnumerator()
            {
                return files.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public sealed class FrameworkManager : IReadOnlyList<GMExtensionFrameworkEntry>
        {
            public int Count => entries.Count;

            public GMExtensionFrameworkEntry this[int index] => entries[index];

            private readonly List<GMExtensionFrameworkEntry> entries;
            private readonly GMExtension extension;

            internal FrameworkManager(GMExtension extension)
            {
                if (extension == null)
                    throw new ArgumentNullException(nameof(extension));

                entries = new List<GMExtensionFrameworkEntry>();

                this.extension = extension;
            }

            public GMExtensionFrameworkEntry Add(string framework)
            {
                if (framework == null)
                    throw new ArgumentNullException(nameof(framework));

                var entry = new GMExtensionFrameworkEntry
                {
                    Project = extension.Project,
                    Id = Guid.NewGuid(),
                    FrameworkName = framework,
                    WeakReference = false
                };

                entries.Add(entry);

                return entry;
            }

            public void Delete(GMExtensionFrameworkEntry entry)
            {
                entries.Remove(entry);
            }

            internal List<GMExtensionFrameworkEntryModel> Serialize()
            {
                return entries.Select(x => (GMExtensionFrameworkEntryModel)x.Serialize()).ToList();
            }

            public IEnumerator<GMExtensionFrameworkEntry> GetEnumerator()
            {
                return entries.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }

    public sealed class GMExtensionFile : ControllerBase
    {
        private string filename;
        public string Filename
        {
            get { return GetProperty(filename); }
            private set { SetProperty(value, ref filename); }
        }
        
        private GMExtensionFunction initFunction;
        public GMExtensionFunction InitFunction
        {
            get { return GetProperty(initFunction); }
            set { SetProperty(value, ref initFunction); }
        }
        
        private GMExtensionFunction finalFunction;
        public GMExtensionFunction FinalFunction
        {
            get { return GetProperty(finalFunction); }
            set { SetProperty(value, ref finalFunction); }
        }
        
        private bool uncompress;
        public bool Uncompress
        {
            get { return GetProperty(uncompress); }
            set { SetProperty(value, ref uncompress); }
        }

        private TargetPlatforms targetPlatforms;
        public TargetPlatforms TargetPlatforms
        {
            get { return GetProperty(targetPlatforms); }
            set { SetProperty(value, ref targetPlatforms); }
        }

        public FunctionManager Functions { get; }
        
        public ConstantManager Constants { get; }
        
        public ProxyFileManager ProxyFiles { get; }

        internal ExtensionKind Kind => ResolveKindFromFilename();

        private string FullFilePath => Path.Combine(
            Project.RootDirectory,
            $@"extensions\{extension.Name}\{Filename}"
        );

        private string pendingFilePath;

        internal readonly GMExtension extension;

        internal GMExtensionFile(GMExtension extension)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            Functions = new FunctionManager(this);
            Constants = new ConstantManager(this);
            ProxyFiles = new ProxyFileManager(this);

            this.extension = extension;
        }

        public void SetFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            Filename = Path.GetFileName(path);
            pendingFilePath = path;
        }

        public void CreateNewFile(string fileName)
        {
            Filename = fileName;
            File.Create(FullFilePath);
        }

        public FileStream GetFileStream()
        {
            string filePath = pendingFilePath ?? FullFilePath;
            return File.Exists(filePath) ? File.OpenRead(filePath) : null;
        }

        internal override ModelBase Serialize()
        {
            if (pendingFilePath != null)
            {
                File.Copy(pendingFilePath, FullFilePath, true);
                pendingFilePath = null;
            }

            return new GMExtensionFileModel
            {
                id = Id,
                filename = Filename,
                origname = "",
                init = InitFunction?.Name ?? "",
                final = FinalFunction?.Name ?? "",
                kind = ResolveKindFromFilename(),
                uncompress = Uncompress,
                functions = Functions.Serialize(),
                constants = Constants.Serialize(),
                ProxyFiles = ProxyFiles.Serialize(),
                copyToTarget = TargetPlatforms,
                order = Functions.Select(x => x.Id).ToList()
            };
        }

        private ExtensionKind ResolveKindFromFilename()
        {
            if (Filename == null)
                return ExtensionKind.Undefined;
            
            string ext = Path.GetExtension(Filename).ToLowerInvariant();
            switch (ext)
            {
                case ".dll": return ExtensionKind.Dll;
                case ".gml": return ExtensionKind.Gml;
                case ".lib": return ExtensionKind.Lib;
                case ".js":  return ExtensionKind.Js;
            }

            return ExtensionKind.Other;
        }

        public sealed class FunctionManager : IReadOnlyList<GMExtensionFunction>
        {
            public int Count => functions.Count;

            public GMExtensionFunction this[int index] => functions[index];

            private readonly List<GMExtensionFunction> functions;
            private readonly GMExtensionFile extensionFile;

            internal FunctionManager(GMExtensionFile extensionFile)
            {
                if (extensionFile == null)
                    throw new ArgumentNullException(nameof(extensionFile));

                functions = new List<GMExtensionFunction>();

                this.extensionFile = extensionFile;
            }

            public GMExtensionFunction Create(string name)
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(name));

                var function = new GMExtensionFunction(extensionFile)
                {
                    Project = extensionFile.Project,
                    Id = Guid.NewGuid(),
                    ExternalName = name,
                    Name = name
                };

                functions.Add(function);

                return function;
            }

            public void Delete(GMExtensionFunction function)
            {
                functions.Remove(function);
            }

            internal List<GMExtensionFunctionModel> Serialize()
            {
                return functions.Select(x => (GMExtensionFunctionModel)x.Serialize()).ToList();
            }

            public IEnumerator<GMExtensionFunction> GetEnumerator()
            {
                return functions.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public sealed class ConstantManager : IReadOnlyList<GMExtensionConstant>
        {
            public int Count => constants.Count;

            public GMExtensionConstant this[int index] => constants[index];

            private readonly List<GMExtensionConstant> constants;
            private readonly GMExtensionFile extensionFile;

            internal ConstantManager(GMExtensionFile extensionFile)
            {
                if (extensionFile == null)
                    throw new ArgumentNullException(nameof(extensionFile));

                constants = new List<GMExtensionConstant>();

                this.extensionFile = extensionFile;
            }

            public GMExtensionConstant Create(string name, string value)
            {
                var constant = new GMExtensionConstant
                {
                    Project = extensionFile.Project,
                    Id = Guid.NewGuid(),
                    Name = name,
                    Value = value
                };

                constants.Add(constant);

                return constant;
            }

            public GMExtensionConstant Get(string name)
            {
                return constants.FirstOrDefault(x => x.Name == name);
            }

            public void Delete(GMExtensionConstant constant)
            {
                constants.Remove(constant);
            }

            internal List<GMExtensionConstantModel> Serialize()
            {
                return constants.Select(x => (GMExtensionConstantModel)x.Serialize()).ToList();
            }

            public IEnumerator<GMExtensionConstant> GetEnumerator()
            {
                return constants.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public sealed class ProxyFileManager : IReadOnlyList<GMExtensionProxyFile>
        {
            public int Count => proxyFiles.Count;

            public GMExtensionProxyFile this[int index] => proxyFiles[index];

            private readonly List<GMExtensionProxyFile> proxyFiles;
            private readonly GMExtensionFile extensionFile;

            internal ProxyFileManager(GMExtensionFile extensionFile)
            {
                if (extensionFile == null)
                    throw new ArgumentNullException(nameof(extensionFile));

                proxyFiles = new List<GMExtensionProxyFile>();

                this.extensionFile = extensionFile;
            }

            public GMExtensionProxyFile CreateFromDisk(string path, TargetPlatforms targetPlatform)
            {
                if (path == null)
                    throw new ArgumentNullException(nameof(path));

                var proxyFile = new GMExtensionProxyFile(extensionFile)
                {
                    Project = extensionFile.Project,
                    Id = Guid.NewGuid(),
                    TargetPlatform = targetPlatform
                };

                proxyFile.SetFile(path);

                proxyFiles.Add(proxyFile);

                return proxyFile;
            }

            public void Remove(GMExtensionProxyFile proxyFile)
            {
                proxyFiles.Remove(proxyFile);
            }

            internal List<GMProxyFileModel> Serialize()
            {
                return proxyFiles.Select(x => (GMProxyFileModel)x.Serialize()).ToList();
            }

            public IEnumerator<GMExtensionProxyFile> GetEnumerator()
            {
                return proxyFiles.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }

    public sealed class GMExtensionFrameworkEntry : ControllerBase
    {
        private string frameworkName;
        public string FrameworkName
        {
            get { return GetProperty(frameworkName); }
            set { SetProperty(value, ref frameworkName); }
        }

        private bool weakReference;
        public bool WeakReference
        {
            get { return GetProperty(weakReference); }
            set { SetProperty(value, ref weakReference); }
        }

        internal override ModelBase Serialize()
        {
            return new GMExtensionFrameworkEntryModel
            {
                id = Id,
                frameworkName = FrameworkName,
                weakReference = WeakReference
            };
        }
    }

    public sealed class GMExtensionFunction : ControllerBase
    {
        private string externalName;
        public string ExternalName
        {
            get { return GetProperty(externalName); }
            set { SetProperty(value, ref externalName); }
        }

        private string name;
        public string Name
        {
            get { return GetProperty(name); }
            set { SetProperty(value, ref name); }
        }

        private string help;
        public string Help
        {
            get { return GetProperty(help); }
            set { SetProperty(value, ref help); }
        }

        private VariableType returnType;
        public VariableType ReturnType
        {
            get { return GetProperty(returnType); }
            set { SetProperty(value, ref returnType); }
        }

        private List<VariableType> arguments;
        public List<VariableType> Arguments
        {
            get { return GetProperty(arguments); }
            set { SetProperty(value, ref arguments); }
        }

        public bool variableArguments;
        public bool VariableArguments
        {
            get { return GetProperty(variableArguments); }
            set { SetProperty(value, ref variableArguments); }
        }

        private readonly GMExtensionFile extensionFile;

        internal GMExtensionFunction(GMExtensionFile extensionFile)
        {
            if (extensionFile == null)
                throw new ArgumentNullException(nameof(extensionFile));

            ExternalName = "";
            Name = "";
            Help = "";
            ReturnType = VariableType.String;
            Arguments = new List<VariableType>();
            VariableArguments = false;

            this.extensionFile = extensionFile;
        }

        internal override ModelBase Serialize()
        {
            return new GMExtensionFunctionModel
            {
                id = Id,
                externalName = ExternalName,
                kind = extensionFile.Kind,
                name = Name,
                help = Help,
                hidden = false,
                returnType = ReturnType,
                argCount = VariableArguments ? -1 : Arguments.Count,
                args = Arguments
            };
        }
    }

    public sealed class GMExtensionConstant : ControllerBase
    {
        private string name;
        public string Name
        {
            get { return GetProperty(name); }
            set { SetProperty(value, ref name); }
        }

        private string value_;
        public string Value
        {
            get { return GetProperty(value_); }
            set { SetProperty(value, ref value_); }
        }

        internal override ModelBase Serialize()
        {
            return new GMExtensionConstantModel
            {
                id = Id,
                constantName = Name,
                value = Value
            };
        }
    }

    public sealed class GMExtensionProxyFile : ControllerBase
    {
        private string filename;
        public string Filename
        {
            get { return GetProperty(filename); }
            set { SetProperty(value, ref filename); }
        }

        private TargetPlatforms targetPlatforms;
        public TargetPlatforms TargetPlatform
        {
            get { return GetProperty(targetPlatforms); }
            set { SetProperty(value, ref targetPlatforms); }
        }

        private string FullProxyFilePath => Path.Combine(
            extensionFile.Project.RootDirectory,
            $@"extensions\{extensionFile.extension.Name}\{Filename}"
        );

        private string pendingProxyFilePath;

        private readonly GMExtensionFile extensionFile;

        internal GMExtensionProxyFile(GMExtensionFile extensionFile)
        {
            if (extensionFile == null)
                throw new ArgumentNullException(nameof(extensionFile));

            this.extensionFile = extensionFile;
        }

        public void SetFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            Filename = Path.GetFileName(path);
            pendingProxyFilePath = path;
        }

        public FileStream GetFileStream()
        {
            string proxyFilePath = pendingProxyFilePath ?? FullProxyFilePath;
            return File.Exists(proxyFilePath) ? File.OpenRead(proxyFilePath) : null;
        }

        internal override ModelBase Serialize()
        {
            if (pendingProxyFilePath != null)
            {
                File.Copy(pendingProxyFilePath, FullProxyFilePath, true);
                pendingProxyFilePath = null;
            }

            return new GMProxyFileModel
            {
                id = Id,
                proxyName = Filename,
                TargetMask = TargetPlatform
            };
        }
    }
}
