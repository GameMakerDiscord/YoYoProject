using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using YoYoProject.Models;
using YoYoProject.Utility;

namespace YoYoProject
{
    internal sealed class ConfigDelta
    {
        private readonly static Version Version = new Version(1, 0, 0);
        private const char MetadataSep = '\u2190';
        private const char DataPairSep = '|';
        
        private readonly ModelBase model;

        public ConfigDelta(ModelBase model)
        {
            this.model = model;
        }

        public void SerializeToFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var builder = new StringBuilder();
            builder.AppendFormat(Version.ToString(3));
            builder.Append(MetadataSep);
            builder.Append(model.id.ToString("D"));
            builder.Append(MetadataSep);

            // TODO Serialize sub-models with deltas
            builder.Append(model.id.ToString("D"));
            builder.Append(DataPairSep);
            builder.Append(SerializeModel(model));

            var contents = builder.ToString();

            File.WriteAllText(path, contents, Encoding.UTF8);
        }

        // TODO Delta parentModel and model, only take deltas...
        private static string SerializeModel(ModelBase model)
        {
            var type = model.GetType();
            var properties = new JsonDictionary();
            
            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (prop.GetCustomAttribute<DataMemberAttribute>() == null)
                    continue;

                if (IgnoredModelProperties.Contains(prop.Name))
                    continue;

                object value = prop.GetValue(model);
                if (value == null)
                    continue;

                properties.Add(prop.Name, value);
            }

            return Json.Serialize(properties);
        }

        private readonly static HashSet<string> IgnoredModelProperties = new HashSet<string>
        {
            "id",
            "mvc",
            "modelName",
            "name"
        };
        
    }
}