using System;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMSoundModel : GMResourceModel
    {
        [DataMember]
        public GMSoundCompression kind { get; set; }

        [DataMember]
        public float volume { get; set; }

        [DataMember]
        public bool preload { get; set; }

        [DataMember]
        public int bitRate { get; set; }

        [DataMember]
        public int sampleRate { get; set; }

        [DataMember]
        public GMSoundType type { get; set; }

        [DataMember]
        public GMSoundBitDepth bitDepth { get; set; }

        [DataMember]
        public Guid audioGroundGuid { get; set; }

        public GMSoundModel()
            : base("GMSound", "1.0")
        {
            
        }
    }

    public enum GMSoundCompression
    {
        Uncompressed,
        Compressed,
        DecompressOnLoad,
        CompressedAndStreamed
    }

    public enum GMSoundType
    {
        Mono,
        Stereo,
        ThreeDimensional
    }

    public enum GMSoundBitDepth
    {
        EightBit,
        SixteenBit
    }
}
