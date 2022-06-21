using System;
using System.Collections.Generic;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using UnityEditor;

namespace HeurekaGames.Utils
{
    public class Heureka_Serializer
    {
        public static string Serialize(List<string> items)
        {
            return JsonUtility.ToJson(new StringList(items));
        }

        public static List<string> DeserializeStringList(string json)
        {
            StringList list = JsonUtility.FromJson<StringList>(json);

            return (list != null) ? list.Items : new List<string>();
        }

        public static Type DeSerializeType(string serializedType)
        {
            return Type.GetType(serializedType);
        }

        public static string SerializeType(Type type)
        {
            return type.AssemblyQualifiedName;
        }

        [SerializeField]
        public class StringList
        {
            public List<string> Items = new List<string>();

            public StringList(List<string> items)
            {
                this.Items = items;
            }
        }
    }
    public static class Heureka_Utils
    {
        public static PackageInfo GetPackageInfo<T>()
        {
            var assembly = typeof(T).Assembly;
            return PackageInfo.FindForAssembly(assembly);
        }

        public static string GetVersionNumber<T>()
        {
            return GetPackageInfo<T>().version;
        }

        public static PackageInfo GetPackageInfoFromObject(UnityEngine.Object asset)
        {
            return PackageInfo.FindForAssetPath(AssetDatabase.GetAssetPath(asset));
        }
    }
}