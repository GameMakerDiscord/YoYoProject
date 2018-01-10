using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using YoYoProject.Controllers;
using YoYoProject.Models;
using YoYoProject.Utility;

namespace YoYoProject
{
    internal sealed class ConfigDelta
    {
        private readonly static Version Version = new Version(1, 0, 0);
        private const char MetadataSep = '\u2190';
        private const char DataPairSep = '|';
        
        public static void SerializeToFile(string path, GMResource resource, ConfigTree.Node config)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            resource.Project.Configs.Active = config;           // HACK PLEASE AVERT YOUR EYES
            var model = resource.Serialize();
            resource.Project.Configs.Active = config.Parent;    // HACK PLEASE AVERT YOUR EYES ONCE MORE
            var parentModel = resource.Serialize();

            var builder = new StringBuilder();
            builder.AppendFormat(Version.ToString(3));
            builder.Append(MetadataSep);
            builder.Append(resource.Id.ToString("D"));
            builder.Append(MetadataSep);

            // TODO Serialize sub-models with deltas
            builder.Append(resource.Id.ToString("D"));
            builder.Append(DataPairSep);
            builder.Append(SerializeModel(model, parentModel));

            var contents = builder.ToString();

            File.WriteAllText(path, contents, Encoding.UTF8);
        }
        
        private static string SerializeModel(ModelBase model, ModelBase parentModel)
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

                object parentValue = prop.GetValue(parentModel);
                if (parentValue != null && value.Equals(parentValue))
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