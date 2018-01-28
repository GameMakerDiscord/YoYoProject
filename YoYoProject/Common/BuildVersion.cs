using System.Runtime.Serialization;

namespace YoYoProject.Common
{
    [DataContract]
    [KnownType(typeof(BuildVersion))]
    public sealed class BuildVersion
    {
        [DataMember]
        public int major { get; set; }

        [DataMember]
        public int minor { get; set; }

        [DataMember]
        public int build { get; set; }

        [DataMember]
        public int revision { get; set; }

        public BuildVersion()
            : this(1, 0, 0, 0)
        {
            
        }

        public BuildVersion(int major, int minor, int build, int revision)
        {
            this.major = major;
            this.minor = minor;
            this.build = build;
            this.revision = revision;
        }

        public BuildVersion(int major, int minor, int build)
            : this(major, minor, build, 0)
        {
            
        }

        public override string ToString()
        {
            return $"{major}.{minor}.{revision}";
        }
    }
}