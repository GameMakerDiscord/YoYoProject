using System;
using System.Runtime.Serialization;

namespace YoYoProject.Models
{
    [DataContract]
    internal sealed class GMSoundModel : GMResourceModel
    {
        public GMSoundCompression kind { get; set; }

        public float volume { get; set; }

        public bool preload { get; set; }

        public int bitRate { get; set; }

        public int sampleRate { get; set; }

        public GMSoundType type { get; set; }

        public GMSoundBitDepth bitDepth { get; set; }

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
