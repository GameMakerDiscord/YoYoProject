using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMFontModel : GMResourceModel
    {
        [DataMember]
        public string fontName { get; set; }

        [DataMember]
        public string styleName { get; set; }

        [DataMember]
        public int size { get; set; }

        // NOTE Deprecated
        [DataMember]
        public bool bold { get; set; }

        // NOTE Deprecated
        [DataMember]
        public bool italic { get; set; }

        // NOTE Deprecated
        [DataMember]
        public int charset { get; set; }

        [DataMember]
        public GMFontAntiAliasMode AntiAlias { get; set; }

        // NOTE Deprecated
        [DataMember]
        public int first { get; set; }

        // NOTE Deprecated
        [DataMember]
        public int last { get; set; }

        [DataMember]
        public string sampleText { get; set; }

        [DataMember]
        public bool includeTTF { get; set; }

        [DataMember]
        public string TTFName { get; set; }

        [DataMember]
        public Guid textureGroupId { get; set; }

        // NOTE Deprecated
        [DataMember]
        public object image { get; set; }

        [DataMember]
        public Dictionary<int, GMGlyphModel> glyphs { get; set; }

        [DataMember]
        public List<GMKerningPairModel> kerningPairs { get; set; }

        [DataMember]
        public List<GMFontRangeModel> ranges { get; set; }

        public GMFontModel()
            : base("GMFont", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMGlyphModel : ModelBase
    {
        [DataMember]
        public int x { get; set; }

        [DataMember]
        public int y { get; set; }

        [DataMember]
        public int w { get; set; }

        [DataMember]
        public int h { get; set; }

        [DataMember]
        public int character { get; set; }

        [DataMember]
        public int shift { get; set; }

        [DataMember]
        public int offset { get; set; }

        public GMGlyphModel()
            : base("GMGlyph", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMKerningPairModel : ModelBase
    {
        [DataMember]
        public int first { get; set; }

        [DataMember]
        public int second { get; set; }

        [DataMember]
        public int amount { get; set; }

        public GMKerningPairModel()
            : base("GMKerningPair", "1.0")
        {
            
        }
    }

    [DataContract]
    internal sealed class GMFontRangeModel
    {
        [DataMember]
        public int x;

        [DataMember]
        public int y;
    }

    public enum GMFontAntiAliasMode
    {
        Disabled,
        Enabled
    }
}
