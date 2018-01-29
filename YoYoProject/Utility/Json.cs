using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using YoYoProject.Models;

namespace YoYoProject.Utility
{
    [DebuggerStepThrough]
    public static class Json
    {
        public static bool PrettyPrint = true;
        public static int IndentLength = 4;

        private readonly static Encoding Encoding = Encoding.Default;

        public static void SerializeToFile<T>(string path, T value)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            File.WriteAllText(
                path,
                Serialize(value),
                Encoding
            );
        }

        public static string Serialize<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            using (var stream = new MemoryStream())
            {
                var json = CreateSerialize(value.GetType());
                json.WriteObject(stream, value);

                var serializedJson = Encoding.GetString(stream.ToArray());
                if (PrettyPrint)
                    serializedJson = PrettyPrintJson(serializedJson);

                return serializedJson;
            }
        }

        public static object Deserialize(Type type, string path)
        {
            using (var stream = File.OpenRead(path))
                return Deserialize(type, stream);
        }

        public static T Deserialize<T>(string path)
            where T : new()
        {
            using (var stream = File.OpenRead(path))
                return Deserialize<T>(stream);
        }

        public static object Deserialize(Type type, Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            var json = CreateSerialize(type);
            return json.ReadObject(stream);
        }

        public static T Deserialize<T>(Stream stream)
            where T : new()
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            var json = CreateSerialize(typeof(T));
            return (T)json.ReadObject(stream);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static DataContractJsonSerializer CreateSerialize(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new DataContractJsonSerializer(
                type,
                new DataContractJsonSerializerSettings
                {
                    DateTimeFormat = new DateTimeFormat("yyyy-mm-dd hh:MM:ss"),
                    IgnoreExtensionDataObject = true,
                    EmitTypeInformation = EmitTypeInformation.Never,
                    KnownTypes = new []
                    {
                        typeof(List<string>)
                    }
                }
            );
        }

        private static string PrettyPrintJson(string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder(str.Length);

            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            sb.Append(new string(' ', ++indent * IndentLength));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            sb.Append(new string(' ', --indent * IndentLength));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            sb.Append(new string(' ', indent * IndentLength));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(' ');
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }

            return sb.ToString();
        }
    }
    
    // NOTE Do not add IDictionary to this class, for whatever reason this will cause the
    //      serializer to throw out our hard work to make it behave itself
    [Serializable]
    [DebuggerStepThrough]
    public class JsonDictionary : ISerializable, IEnumerable
    {
        public ICollection<string> Keys => dictionary.Keys;

        public ICollection<object> Values => dictionary.Values;

        public int Count => dictionary.Count;

        public object this[string key]
        {
            get { return dictionary[key]; }
            set { dictionary[key] = value; }
        }

        private readonly Dictionary<string, object> dictionary;

        public JsonDictionary()
        {
            dictionary = new Dictionary<string, object>();
        }

        protected JsonDictionary(SerializationInfo info, StreamingContext context)
        {
            foreach (var entry in info)
                dictionary.Add(entry.Name, entry.Value);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            foreach (string key in dictionary.Keys)
                info.AddValue(key, dictionary[key]);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(string key, object value)
        {
            dictionary.Add(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(string key)
        {
            return dictionary.Remove(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            dictionary.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(string key, out object value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(string key)
        {
            return dictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
