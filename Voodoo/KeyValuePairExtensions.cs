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
            if (list.ContainsKey(key))
            {
                var nvp = list.FirstOrDefault(e => e.Key == key);
                list.Remove(nvp);
            }
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
            if (list.ContainsKey(key))
            {
                return list.To<List<IKeyValuePair>>().Any(e => e.Key == key && e.Value.Contains(value));
            }
            return false;
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

        //public static SelectList AsSelectList(this List<INameValuePair> list, string selectedValue = null)
        //{
        //    return new SelectList(list, "Value", "Name", selectedValue);
        //}
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

        public static string GetValue(this IEnumerable<IKeyValuePair> list, string name)
        {
            return list.SingleOrDefault(e => e.Key == name).To<string>();
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