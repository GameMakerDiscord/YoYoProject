using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace YoYoProject
{
    [DebuggerStepThrough]
    internal static class Json
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

        public static T Deserialize<T>(string path)
            where T : new()
        {
            using (var stream = File.OpenRead(path))
                return Deserialize<T>(stream);
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
                    DataContractSurrogate = new GMProjectSurrogate(),
                    UseSimpleDictionaryFormat = true,
                    DateTimeFormat = new DateTimeFormat("yyyy-mm-dd hh:MM:ss"),
                    IgnoreExtensionDataObject = true,
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
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }

            return sb.ToString();
        }

        private sealed class GMProjectSurrogate : IDataContractSurrogate
        {
            public Type GetDataContractType(Type type)
            {
                if (type == typeof(GMResourceInfoModel))
                    return typeof(Dictionary<string, object>);

                return type;
            }

            public object GetObjectToSerialize(object obj, Type targetType)
            {
                var resourceInfoModel = obj as GMResourceInfoModel;
                if (resourceInfoModel != null)
                    return SerializeResourceInfo(resourceInfoModel);

                return obj;
            }

            public object GetDeserializedObject(object obj, Type targetType)
            {
                return obj;
            }

            public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
            {
                return null;
            }

            public object GetCustomDataToExport(Type clrType, Type dataContractType)
            {
                return null;
            }

            public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
            {
                
            }

            public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
            {
                return null;
            }

            public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
            {
                return typeDeclaration;
            }

            private static Dictionary<string, object> SerializeResourceInfo(GMResourceInfoModel gmResourceInfo)
            {
                var dict = new Dictionary<string, object>
                {
                    { "id", gmResourceInfo.id },
                    { "resourcePath", gmResourceInfo.ResourcePath },
                    { "resourceType", gmResourceInfo.ResourceType }
                };

                if (gmResourceInfo.ConfigDeltaFiles != null)
                    dict.Add("configDeltaFiles", gmResourceInfo.ConfigDeltaFiles);

                if (gmResourceInfo.ConfigDeltas != null)
                    dict.Add("configDeltas", gmResourceInfo.ConfigDeltas);

                if (gmResourceInfo.ResourceCreationConfigs != null)
                    dict.Add("resourceCreationConfigs", gmResourceInfo.ResourceCreationConfigs);

                return dict;
            }
        }
    }
}
