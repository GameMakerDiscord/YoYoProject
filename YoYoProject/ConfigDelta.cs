using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace YoYoProject
{
    internal sealed class ConfigDelta
    {
        private readonly static Version Version = new Version(1, 0, 0);
        private const char MetadataSep = '\u2190';
        private const char DataPairSep = '|';

        public string Name { get; }

        private readonly Dictionary<string, object> properties;
        private readonly ControllerBase controller;

        public ConfigDelta(string name, ControllerBase controller)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (controller == null)
                throw new ArgumentNullException(nameof(controller));

            Name = name;
            this.controller = controller;
            properties = new Dictionary<string, object>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetProperty(string key, out object value)
        {
            return properties.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetProperty(string key, object value)
        {
            properties[key] = value;
        }

        public void SerializeToFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var builder = new StringBuilder();
            builder.AppendFormat(Version.ToString(3));
            builder.Append(MetadataSep);
            builder.Append(controller.Id.ToString("D"));
            builder.Append(MetadataSep);

            // TODO Serialize sub-models with deltas
            builder.Append(controller.Id.ToString("D"));
            builder.Append(DataPairSep);
            builder.Append(Json.Serialize(properties));

            var contents = builder.ToString();

            File.WriteAllText(path, contents, Encoding.UTF8);
        }
    }
}