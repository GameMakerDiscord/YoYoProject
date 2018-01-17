using System;
using System.Collections.Generic;
using System.Linq;
using YoYoProject.Common;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMMainOptions : GMResource
    {
        private Guid gameGuid;
        public Guid GameGuid
        {
            get { return GetProperty(gameGuid); }
            set { SetProperty(value, ref gameGuid); }
        }
        
        private int gameSpeed;
        public int GameSpeed
        {
            get { return GetProperty(gameSpeed); }
            set { SetProperty(value, ref gameSpeed); }
        }
        
        private bool useMipsFor3DTextures;
        public bool UseMipsFor3DTextures
        {
            get { return GetProperty(useMipsFor3DTextures); }
            set { SetProperty(value, ref useMipsFor3DTextures); }
        }
        
        private Color drawColor;
        public Color DrawColor
        {
            get { return GetProperty(drawColor); }
            set { SetProperty(value, ref drawColor); }
        }
        
        private string steamAppId;
        public string SteamAppId
        {
            get { return GetProperty(steamAppId); }
            set { SetProperty(value, ref steamAppId); }
        }
        
        private bool allowGameStatistics;
        public bool AllowGameStatistics
        {
            get { return GetProperty(allowGameStatistics); }
            set { SetProperty(value, ref allowGameStatistics); }
        }
        
        private bool useSci;
        public bool UseSci
        {
            get { return GetProperty(useSci); }
            set { SetProperty(value, ref useSci); }
        }
        
        private string author;
        public string Author
        {
            get { return GetProperty(author); }
            set { SetProperty(value, ref author); }
        }
        
        private DateTime lastChanged;
        public DateTime LastChanged
        {
            get { return GetProperty(lastChanged); }
            set { SetProperty(value, ref lastChanged); }
        }
        
        private GMGraphicsOptions graphics;
        public GMGraphicsOptions Graphics
        {
            get { return GetProperty(graphics); }
            set { SetProperty(value, ref graphics); }
        }
        
        private GMAudioOptions audio;
        public GMAudioOptions Audio
        {
            get { return GetProperty(audio); }
            set { SetProperty(value, ref audio); }
        }
        
        protected internal override string ResourcePath => @"options\main\options_main.yy";

        public GMMainOptions()
        {
            Graphics = new GMGraphicsOptions();
            Audio = new GMAudioOptions();
        }

        protected internal override void Create()
        {
            GameGuid = Guid.NewGuid();
            GameSpeed = 60;
            UseMipsFor3DTextures = true;
            DrawColor = Color.White;
            SteamAppId = "0";
            AllowGameStatistics = false;
            UseSci = false;
            Author = "";
            LastChanged = DateTime.Now;
        }

        protected internal override ModelBase Serialize()
        {
            return new GMMainOptionsModel
            {
                id = Id,
                name = "Main",
                option_gameguid = GameGuid.ToString("D"),
                option_game_speed = GameSpeed,
                option_mips_for_3d_textures = UseMipsFor3DTextures,
                option_draw_colour = DrawColor,
                option_steam_app_id = SteamAppId,
                option_allow_game_statistics = AllowGameStatistics,
                option_sci_usesci = UseSci,
                option_author = Author,
                option_lastchanged = LastChanged.ToString("dd MMMM YYYY HH:mm:ss"),
                graphics_options = Graphics?.Serialize(),
                audio_options = Audio?.Serialize()
            };
        }
    }

    // TODO Refactor texture group management behind a manager
    public sealed class GMGraphicsOptions
    {
        private const string DefaultName = "Default";

        public Guid Id { get; set; }

        public List<GMTextureGroup> TextureGroups { get; }

        public GMTextureGroup DefaultTextureGroup
        {
            get { return TextureGroups.Single(x => x.Name == DefaultName); }
        }

        public GMGraphicsOptions()
        {
            Id = Guid.NewGuid();
            TextureGroups = new List<GMTextureGroup>();

            CreateTextureGroup(DefaultName, null);
        }

        public GMTextureGroup CreateTextureGroup(string name, GMTextureGroup parent)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            // TODO Validate name

            var textureGroup = new GMTextureGroup
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            TextureGroups.Add(textureGroup);

            return textureGroup;
        }

        internal GMGraphicsOptionsModel Serialize()
        {
            return new GMGraphicsOptionsModel
            {
                id = Id,
                textureGroups = TextureGroups.Select(x => (GMTextureGroupModel)x.Serialize()).ToList()
            };
        }
    }

    // TODO Refactor audio group management behind a manager
    public sealed class GMAudioOptions
    {
        private const string DefaultName = "audiogroup_default";

        public Guid Id { get; set; }

        public List<GMAudioGroup> AudioGroups { get; }

        public GMAudioGroup DefaultAudioGroup
        {
            get { return AudioGroups.Single(x => x.Name == DefaultName); }
        }

        public GMAudioOptions()
        {
            Id = Guid.NewGuid();
            AudioGroups = new List<GMAudioGroup>();

            CreateAudioGroup(DefaultName, null);
        }

        public GMAudioGroup CreateAudioGroup(string name, GMTextureGroup parent)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            // TODO Validate name

            var audioGroup = new GMAudioGroup
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            AudioGroups.Add(audioGroup);

            return audioGroup;
        }

        internal GMAudioOptionsModel Serialize()
        {
            return new GMAudioOptionsModel
            {
                id = Id,
                audioGroups = AudioGroups.Select(x => (GMAudioGroupModel)x.Serialize()).ToList()
            };
        }
    }

    public sealed class GMAudioGroup : GMBaseGroup
    {
        internal override GMBaseGroupModel Serialize()
        {
            return new GMAudioGroupModel
            {
                id = Id,
                groupName = Name,
                targets = Targets
            };
        }
    }

    public sealed class GMTextureGroup : GMBaseGroup
    {
        public GMTextureGroup Parent { get; set; }

        public bool Scaled { get; set; }

        public bool AutoCrop { get; set; }

        public int Border { get; set; }

        public int MipsToGenerate { get; set; }

        public GMTextureGroup()
        {
            Scaled = true;
            AutoCrop = true;
            Border = 2;
            MipsToGenerate = 0;
        }

        internal override GMBaseGroupModel Serialize()
        {
            return new GMTextureGroupModel
            {
                id = Id,
                groupName = Name,
                targets = Targets,
                groupParent = Parent?.Id ?? Guid.Empty,
                scaled = Scaled,
                autocrop = AutoCrop,
                border = Border,
                mipsToGenerate = MipsToGenerate
            };
        }
    }

    public abstract class GMBaseGroup
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TargetPlatforms Targets { get; set; }

        internal abstract GMBaseGroupModel Serialize();
    }
}
