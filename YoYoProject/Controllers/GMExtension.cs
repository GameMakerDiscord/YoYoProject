using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            iOS = new GMExtensionIos();
            Android = new GMExtensionAndroid();
            Files = new FileManager();
        }

        protected internal override void Create()
        {
            throw new NotImplementedException();
        }

        internal override ModelBase Serialize()
        {
            throw new NotImplementedException();
        }

        public sealed class GMExtensionIos : ControllerBase
        {
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

            internal GMExtensionIos()
            {
                SystemFrameworks = new FrameworkManager();
                ThirdPartyFrameworks = new FrameworkManager();
            }

            internal override ModelBase Serialize()
            {
                throw new InvalidOperationException();
            }
        }

        public sealed class GMExtensionAndroid : ControllerBase
        {
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

            internal FileManager()
            {
                files = new List<GMExtensionFile>();
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

            internal FrameworkManager()
            {
                entries = new List<GMExtensionFrameworkEntry>();
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
            set { SetProperty(value, ref filename); }
        }
        
        private string initFunction;
        public string InitFunction
        {
            get { return GetProperty(initFunction); }
            set { SetProperty(value, ref initFunction); }
        }
        
        private string finalFunction;
        public string FinalFunction
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
        
        internal GMExtensionFile()
        {
            Functions = new FunctionManager();
            Constants = new ConstantManager();
            ProxyFiles = new ProxyFileManager();
        }

        internal override ModelBase Serialize()
        {
            throw new NotImplementedException();
        }

        public sealed class FunctionManager : IReadOnlyList<GMExtensionFunction>
        {
            public int Count => functions.Count;

            public GMExtensionFunction this[int index] => functions[index];

            private readonly List<GMExtensionFunction> functions;

            internal FunctionManager()
            {
                functions = new List<GMExtensionFunction>();
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

            internal ConstantManager()
            {
                constants = new List<GMExtensionConstant>();
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

            internal ProxyFileManager()
            {
                proxyFiles = new List<GMExtensionProxyFile>();
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
            throw new NotImplementedException();
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

        internal override ModelBase Serialize()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
        public TargetPlatforms TargetPlatforms
        {
            get { return GetProperty(targetPlatforms); }
            set { SetProperty(value, ref targetPlatforms); }
        }

        internal override ModelBase Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
