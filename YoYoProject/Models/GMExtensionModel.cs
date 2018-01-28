using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using YoYoProject.Common;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMExtensionModel : GMResourceModel
    {
        // NOTE Deprecated
        [DataMember]
        public string extensionName { get; set; }

        [DataMember]
        public string version { get; set; }

        // NOTE Deprecated
        [DataMember]
        public string packageID { get; set; }

        // NOTE Deprecated
        [DataMember]
        public string productID { get; set; }

        // NOTE Deprecated
        [DataMember]
        public string author { get; set; }

        [DataMember]
        public DateTime date { get; set; }

        // NOTE Deprecated
        [DataMember]
        public string license { get; set; }

        // NOTE Deprecated
        [DataMember]
        public string description { get; set; }

        // NOTE Deprecated
        [DataMember]
        public string helpfile { get; set; }

        [DataMember]
        public bool iosProps { get; set; }

        [DataMember]
        public bool androidProps { get; set; }

        // NOTE Deprecated
        [DataMember]
        public string installdir { get; set; }

        [DataMember]
        public List<GMExtensionFileModel> files { get; set; }

        [DataMember]
        public string classname { get; set; }
        
        [DataMember]
        public string androidclassname { get; set; }

        [DataMember]
        public string sourcedir { get; set; }

        [DataMember]
        public string macsourcedir { get; set; }

        [DataMember]
        public string maccompilerflags { get; set; }

        [DataMember]
        public string maclinkerflags { get; set; }

        [DataMember]
        public string iosplistinject { get; set; }

        [DataMember]
        public string androidinject { get; set; }

        [DataMember]
        public string androidmanifestinject { get; set; }

        [DataMember]
        public string androidactivityinject { get; set; }

        [DataMember]
        public string gradleinject { get; set; }

        [DataMember]
        public List<GMExtensionFrameworkEntryModel> iosSystemFrameworkEntries { get; set; }

        [DataMember]
        public List<GMExtensionFrameworkEntryModel> iosThirdPartyFrameworkEntries { get; set; }

        [DataMember]
        public List<string> IncludedResources { get; set; }

        [DataMember]
        public List<string> androidPermissions { get; set; }

        [DataMember]
        public TargetPlatforms copyToTargets { get; set; }

        public GMExtensionModel()
            : base("GMExtension", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMExtensionFileModel : ModelBase
    {
        [DataMember]
        public string filename { get; set; }

        [DataMember]
        public string origname { get; set; }

        [DataMember]
        public string init { get; set; }

        [DataMember]
        public string final { get; set; }

        [DataMember]
        public ExtensionKind kind { get; set; }

        [DataMember]
        public bool uncompress { get; set; }

        [DataMember]
        public List<GMExtensionFunctionModel> functions { get; set; }

        [DataMember]
        public List<GMExtensionConstantModel> constants { get; set; }

        [DataMember]
        public List<GMProxyFileModel> ProxyFiles { get; set; }

        [DataMember]
        public TargetPlatforms copyToTarget { get; set; }

        [DataMember]
        public List<Guid> order { get; set; }
        
        public GMExtensionFileModel()
            : base("GMExtensionFile", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMExtensionFunctionModel : ModelBase
    {
        [DataMember]
        public string externalName { get; set; }

        [DataMember]
        public ExtensionKind kind { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string help { get; set; }

        [DataMember]
        public bool hidden { get; set; }

        [DataMember]
        public VariableType returnType { get; set; }

        [DataMember]
        public int argCount { get; set; }

        [DataMember]
        public List<VariableType> args { get; set; }

        public GMExtensionFunctionModel()
            : base("GMExtensionFunction", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMExtensionConstantModel : ModelBase
    {
        [DataMember]
        public string constantName { get; set; }

        [DataMember]
        public string value { get; set; }

        [DataMember]
        public bool hidden { get; set; }

        public GMExtensionConstantModel()
            : base("GMExtensionConstant", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMProxyFileModel : ModelBase
    {
        [DataMember]
        public string proxyName { get; set; }

        [DataMember]
        public TargetPlatforms TargetMask { get; set; }

        public GMProxyFileModel()
            : base("GMProxyFile", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMExtensionFrameworkEntryModel : ModelBase
    {
        [DataMember]
        public string frameworkName { get; set; }

        [DataMember]
        public bool weakReference { get; set; }

        public GMExtensionFrameworkEntryModel()
            : base("GMExtensionFrameworkEntry", "1.0")
        {
            
        }
    }

    public enum ExtensionKind
    {
        Undefined = 0,
        Dll = 1,
        Gml = 2,
        Lib = 3,
        Other = 4,
        Js = 5,
        Stdcall = 11,
        Cdecl = 12
    }

    public enum VariableType
    {
        String = 0,
        Double = 1
    }
}
