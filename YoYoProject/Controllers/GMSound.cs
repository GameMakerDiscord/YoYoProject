using System;
using System.IO;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMSound : GMResource
    {
        private GMSoundCompression compressionKind;
        public GMSoundCompression CompressionKind
        {
            get { return GetProperty(compressionKind); }
            set { SetProperty(value, ref compressionKind); }
        }
        
        private float volume;
        public float Volume
        {
            get { return GetProperty(volume); }
            set
            {
                if (value < 0)
                    SetProperty(0, ref volume);
                else if (value > 1)
                    SetProperty(1, ref volume);
                else
                    SetProperty(value, ref volume);
            }
        }
        
        private bool preLoad;
        public bool PreLoad
        {
            get { return GetProperty(preLoad); }
            set { SetProperty(value, ref preLoad); }
        }
        
        private int bitRate;
        public int BitRate
        {
            get { return GetProperty(bitRate); }
            set { SetProperty(value, ref bitRate); }
        }
        
        private int sampleRate;
        public int SampleRate
        {
            get { return GetProperty(sampleRate); }
            set { SetProperty(value, ref sampleRate); }
        }
        
        private GMSoundType type;
        public GMSoundType Type
        {
            get { return GetProperty(type); }
            set { SetProperty(value, ref type); }
        }
        
        private GMSoundBitDepth bitDepth;
        public GMSoundBitDepth BitDepth
        {
            get { return GetProperty(bitDepth); }
            set { SetProperty(value, ref bitDepth); }
        }
        
        private GMAudioGroup audioGroup;
        public GMAudioGroup AudioGroup
        {
            get { return GetProperty(audioGroup); }
            set { SetProperty(value, ref audioGroup); }
        }

        internal override string ResourcePath => $@"sounds\{Name}\{Name}.yy";

        private string FullSoundPath => Path.Combine(Project.RootDirectory, $@"sounds\{Name}\{Name}");

        private string pendingSoundPath;

        internal override void Create(string name)
        {
            Name = Project.Resources.GenerateValidName(name ?? "sound");
            CompressionKind = GMSoundCompression.Uncompressed;
            Volume = 1.0f;
            PreLoad = false;
            BitRate = 128;
            SampleRate = 44100;
            Type = GMSoundType.Mono;
            BitDepth = GMSoundBitDepth.SixteenBit;
            AudioGroup = Project.Resources.Get<GMMainOptions>().Audio.DefaultAudioGroup;

            AddResourceToFolder("GMSound");
        }

        public void SetAudioFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            pendingSoundPath = path;
        }

        public void SetAudioFile(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            pendingSoundPath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            File.WriteAllBytes(pendingSoundPath, buffer);
        }

        public FileStream GetAudioFileStream()
        {
            string soundPath = pendingSoundPath ?? FullSoundPath;
            return File.Exists(soundPath) ? File.OpenRead(soundPath) : null;
        }

        internal override ModelBase Serialize()
        {
            if (pendingSoundPath != null)
            {
                File.Copy(pendingSoundPath, FullSoundPath, true);
                pendingSoundPath = null;
            }

            return new GMSoundModel
            {
                id = Id,
                name = Name,
                kind = CompressionKind,
                volume = Volume,
                preload = PreLoad,
                bitRate = BitRate,
                sampleRate = SampleRate,
                type = Type,
                bitDepth = BitDepth,
                audioGroundGuid = AudioGroup?.Id ?? Guid.Empty
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            // TODO Implement
            var soundModel = (GMSoundModel)model;

            Id = soundModel.id;
            Name = soundModel.name;
        }
    }
}
