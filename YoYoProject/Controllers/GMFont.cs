using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMFont : GMResource
    {
        private const string DefaultSampleText = @"abcdef ABCDEF
0123456789 .,<>""'&!?
the quick brown fox jumps over the lazy dog
THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG";

        private string fontName;
        public string FontName
        {
            get { return GetProperty(fontName); }
            set { SetProperty(value, ref fontName); }
        }

        private string styleName;
        public string StyleName
        {
            get { return GetProperty(styleName); }
            set { SetProperty(value, ref styleName); }
        }

        private int size;
        public int Size
        {
            get { return GetProperty(size); }
            set { SetProperty(value, ref size); }
        }

        private GMFontAntiAliasMode antiAlias;
        public GMFontAntiAliasMode AntiAlias
        {
            get { return GetProperty(antiAlias); }
            set { SetProperty(value, ref antiAlias); }
        }

        private string sampleText;
        public string SampleText
        {
            get { return GetProperty(sampleText); }
            set { SetProperty(value, ref sampleText); }
        }

        private GMTextureGroup textureGroup;
        public GMTextureGroup TextureGroup
        {
            get { return GetProperty(textureGroup); }
            set { SetProperty(value, ref textureGroup); }
        }
        
        public RangeManager Ranges { get; }

        internal override string ResourcePath => $@"fonts\{Name}\{Name}.yy";

        public GMFont()
        {
            Ranges = new RangeManager(this);
        }

        internal override void Create(string name)
        {
            Name = Project.Resources.GenerateValidName(name ?? "font");
            FontName = "Arial";
            StyleName = "Regular";
            Size = 12;
            AntiAlias = GMFontAntiAliasMode.Enabled;
            SampleText = DefaultSampleText;
            TextureGroup = Project.Resources.Get<GMMainOptions>().Graphics.DefaultTextureGroup;

            Ranges.Add(GMFontRange.Normal);

            AddResourceToFolder("GMFont");
        }

        internal override ModelBase Serialize()
        {
            // TODO Generate font texture
            // TODO Generate glyphs
            var glyphs = new Dictionary<int, GMGlyphModel>();

            // TODO Generate kerning
            var kerningPairs = new List<GMKerningPairModel>();

            return new GMFontModel
            {
                id = Id,
                name = Name,
                fontName = FontName,
                styleName = StyleName,
                size = Size,
                AntiAlias = AntiAlias,
                sampleText = SampleText,
                includeTTF = false, // TODO Implement
                TTFName = null, // TODO Implement
                glyphs = glyphs,
                kerningPairs = kerningPairs,
                ranges = Ranges.Select(x => x.Serialize()).ToList()
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            // TODO Implement
            var fontModel = (GMFontModel)model;

            Id = fontModel.id;
            Name = fontModel.name;
        }

        public sealed class RangeManager : IReadOnlyList<GMFontRange>
        {
            public int Count => ranges.Count;

            public GMFontRange this[int index] => ranges[index];

            private readonly List<GMFontRange> ranges;
            private readonly GMFont font;

            internal RangeManager(GMFont font)
            {
                if (font == null)
                    throw new ArgumentNullException(nameof(font));

                ranges = new List<GMFontRange>();
                this.font = font;
            }

            public GMFontRange Create(int start, int end)
            {
                var range = new GMFontRange(start, end);
                return Add(range);
            }

            public GMFontRange Add(GMFontRange range)
            {
                if (range == null)
                    throw new ArgumentNullException(nameof(range));

                // TODO Validate ranges don't overlap

                ranges.Add(range);

                return range;
            }

            public void Delete(GMFontRange range)
            {
                ranges.Remove(range);
            }

            public IEnumerator<GMFontRange> GetEnumerator()
            {
                return ranges.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }

    public sealed class GMFontRange
    {
        public readonly static GMFontRange Normal = new GMFontRange(32, 127);
        public readonly static GMFontRange ASCII = new GMFontRange(32, 255);
        public readonly static GMFontRange Digits = new GMFontRange(48, 57);
        public readonly static GMFontRange Letters = new GMFontRange(65, 122);

        public int Start { get; set; }

        public int End { get; set; }

        public GMFontRange(int start, int end)
        {
            Start = start;
            End = end;
        }

        internal GMFontRangeModel Serialize()
        {
            return new GMFontRangeModel
            {
                x = Start,
                y = End
            };
        }
    }
}
