using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
            builder.Append(SerializeModel(model)); // TODO PrettyPrint

            var contents = builder.ToString();

            File.WriteAllText(path, contents, Encoding.UTF8);
        }

        // TODO Delta parentModel and model, only take deltas...
        private static string SerializeModel(ModelBase model)
        {
            var type = model.GetType();
            var properties = new Dictionary<string, object>();

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

            return SerializeDictionary(properties);
        }

        private readonly static HashSet<string> IgnoredModelProperties = new HashSet<string>
        {
            "id",
            "mvc",
            "modelName",
            "name"
        };

        // TODO Would be nice to roll this into a surrogate and if we run into a IDictionary with
        //      some attribute just use this path instead. This is the simplest solution for now
        private static string SerializeDictionary(IDictionary dictionary)
        {
            var builder = new StringBuilder();
            builder.Append('{');

            var entries = dictionary.GetEnumerator();
            entries.MoveNext();

            while (true)
            {
                var entry = entries.Entry;

                var value = SerializeValue(entry.Value);
                builder.AppendFormat("\"{0}\":{1}", entry.Key, value);

                if (!entries.MoveNext())
                    break;

                builder.Append(',');
            }

            builder.Append('}');

            return builder.ToString();
        }

        private static string SerializeValue(object value)
        {
            if (value == null)
                return "null";

            // TODO C# 7.0 Pattern matching would be prime here
            var dictionary = value as IDictionary;
            if (dictionary != null)
                return SerializeDictionary(dictionary);

            var list = value as IList;
            if (list != null)
                return SerializeList(list);

            if (value is Guid)
                return $"\"{(Guid)value}\"";

            if (value is bool)
                return (bool)value ? "true" : "false";

            var @string = value as string;
            if (@string != null)
                return $"\"{@string.Replace("\"", "\\\"")}\"";

            var formattable = value as IFormattable;
            if (formattable != null)
                return Convert.ToString(value, CultureInfo.InvariantCulture);

            var type = value.GetType();
            if (type.GetCustomAttribute<DataContractAttribute>() != null)
                return Json.Serialize(value);

            return "null";
        }

        private static string SerializeList(IList value)
        {
            if (value == null)
                return "null";

            var builder = new StringBuilder();
            builder.Append('[');

            foreach (var item in value)
                builder.Append(SerializeValue(item));

            builder.Append(']');
            return builder.ToString();
        }
    }
}