using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMIncludedFileModel : GMResourceModel
    {
        // NOTE Deprecated
        [DataMember]
        public string origName { get; set; }

        // NOTE Deprecated
        [DataMember]
        public bool exists { get; set; }

        // NOTE Deprecated
        [DataMember]
        public int size { get; set; }

        // NOTE Deprecated
        [DataMember]
        public int exportAction { get; set; }

        // NOTE Deprecated
        [DataMember]
        public string exportDir { get; set; }

        // NOTE Deprecated
        [DataMember]
        public bool overwrite { get; set; }

        // NOTE Deprecated
        [DataMember]
        public bool freeData { get; set; }

        // NOTE Deprecated
        [DataMember]
        public bool removeEnd { get; set; }

        // NOTE Deprecated
        [DataMember]
        public bool store { get; set; }

        [DataMember]
        public TargetPlatforms CopyToMask { get; set; }

        // NOTE Deprecated
        [DataMember]
        public string tags { get; set; }

        [DataMember]
        public string fileName { get; set; }

        // NOTE Deprecated
        [DataMember]
        public string filePath { get; set; }

        public GMIncludedFileModel()
            : base("GMIncludedFile", "1.0")
        {
            
        }
    }
}
