using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Voodoo.Messages;

namespace Voodoo
{
    public static class KeyValuePairExtensions
    {
        public static void Add(this List<IKeyValuePair> list, string key, string value)
        {
            list.Add(new KeyValuePair(key, value));
        }

        public static void SetValue(this List<IKeyValuePair> list, string key, string value)
        {
            if (!list.ContainsKey(key))
                return;
            var pair = list.FirstOrDefault(e => e.Key == key);
            if (pair != null)
                pair.Value = value;
        }

        public static void RemoveValue(this List<IKeyValuePair> list, string key)
        {
            if (!list.ContainsKey(key))
                return;
            var pair = list.FirstOrDefault(e => e.Key == key);
            list.Remove(pair);
        }

        public static string GetValue(this IEnumerable<IKeyValuePair> list, string key)
        {
            return list.SingleOrDefault(e => e.Key == key).To<string>();
        }

        public static bool ContainsValue(this List<IKeyValuePair> list, string value)
        {
            return list.To<List<IKeyValuePair>>().Any(c => c.Value == value);
        }

        public static bool ContainsKey(this List<IKeyValuePair> list, string key)
        {
            return list.To<List<IKeyValuePair>>().Any(c => c.Key == key);
        }

        public static bool Contains(this List<IKeyValuePair> list, string key, string value)
        {
            return list.ContainsKey(key) &&
                   list.To<List<IKeyValuePair>>().Any(e => e.Key == key && e.Value.Contains(value));
        }

        public static List<IKeyValuePair> Without(this List<IKeyValuePair> list, string key)
        {
            return list.To<List<IKeyValuePair>>().Where(e => e.Key != key).ToList();
        }

        public static List<IKeyValuePair> ToINameValuePairList(this Dictionary<string, string> items)
        {
            if (items == null)
                return new List<IKeyValuePair>();

            var ret = items.Select(e => (IKeyValuePair) new KeyValuePair(e.Value, e.Key)).ToList();
            return ret;
        }

        public static List<IKeyValuePair> ToIKeyValuePairList(this Type enumeration)
        {
            if (enumeration.BaseType != typeof (Enum))
            {
                throw new ArgumentException("enumeration must be an enum");
            }
            var ret =
                Enum.GetNames(enumeration)
                    .Select(
                        e =>
                            (IKeyValuePair)
                                new KeyValuePair(e.ToFriendlyString(), ((int) Enum.Parse(enumeration, e)).ToString()))
                    .ToList();
            return ret;
        }

        public static List<IKeyValuePair> ToIKeyValuePairListWithUnfriendlyNames(this Type enumeration)
        {
            if (enumeration.BaseType != typeof (Enum))
            {
                throw new ArgumentException("enumeration must be an enum");
            }
            var ret =
                Enum.GetNames(enumeration)
                    .Select(e => (IKeyValuePair) new KeyValuePair(e, ((int) Enum.Parse(enumeration, e)).ToString()))
                    .ToList();
            return ret;
        }

        public static IEnumerable<IKeyValuePair> AsEnumerable(this NameValueCollection nvc)
        {
            var result = new List<IKeyValuePair>();
            for (var i = 0; i < nvc.Count; i++)
            {
                result.Add(new KeyValuePair(nvc.Keys[i], nvc[nvc.Keys[i]]));
            }
            return result.AsEnumerable();
        }
    }
}