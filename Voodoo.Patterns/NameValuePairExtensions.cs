using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
#if !PCL
using System.ComponentModel.DataAnnotations;
#endif
using System.Linq;
using System.Reflection;
using Voodoo.Messages;

namespace Voodoo
{
    public static class NameValuePairExtensions
    {
        public static void Add(this IList<INameValuePair> list, string name, string value)
        {
            list.Add(new NameValuePair {Name = name, Value = value});
        }

        public static void SetValue(this IList<INameValuePair> list, string name, string value)
        {
            if (!list.ContainsName(name))
                return;
            var pair = list.FirstOrDefault(e => e.Name == name);
            if (pair != null)
                pair.Value = value;
        }

        public static void RemoveByName(this IList<INameValuePair> list, string name)
        {
            if (!list.ContainsName(name))
                return;
            var pair = list.Where(e => e.Name == name).ToArray();
            pair.ForEach(c => list.Remove(c));
        }

        public static void RemoveByValue(this IList<INameValuePair> list, string value)
        {
            if (!list.ContainsValue(value))
                return;
            var pair = list.Where(e => e.Value == value).ToArray();
            pair.ForEach(c => list.Remove(c));
        }

        public static string GetValue(this IEnumerable<INameValuePair> list, string name)
        {
	        var result = list?.FirstOrDefault(e => e.Name == name);
	        return result?.Value;
        }

        public static bool ContainsValue(this IEnumerable<INameValuePair> list, string value)
        {
            if (list == null)
                return false;

            return list.ToList().Any(c => c.Value == value);
        }

        public static bool ContainsName(this IEnumerable<INameValuePair> list, string name)
        {
            if (list == null)
                return false;
            return list.ToList().Any(c => c.Name == name);
        }

        public static bool ContainsItem(this IEnumerable<INameValuePair> list, string name, string value)
        {
            return list.ToList().Any(e => e.Name == name && e.Value == value);
        }

        public static IList<INameValuePair> Without(this IEnumerable<INameValuePair> list, string name)
        {
            return list.ToList().Where(e => e.Name != name).ToList();
        }

        public static IList<INameValuePair> ToINameValuePairList<TKey, TValue>(this Dictionary<TKey, TValue> items)
        {
            if (items == null)
                return new List<INameValuePair>();

            var result =
                items.Select(e => (INameValuePair) new NameValuePair(e.Key.To<string>(), e.Value.To<string>())).ToList();
            return result;
        }

        public static IList<INameValuePair> ToINameValuePairList(this Type enumeration)
        {
            if (enumeration.GetTypeInfo().BaseType != typeof (Enum))
            {
                throw new ArgumentException(Strings.Validation.enumerationMustBeAnEnum);
            }
            var result =
                Enum.GetNames(enumeration)
                    .Select(
                        e =>
                            (INameValuePair)
                                new NameValuePair(
									Enum.Parse(enumeration, e).ToFriendlyString() ,((int) Enum.Parse(enumeration, e)).ToString()))
                    .ToList();
            return result;
        }
	
	    public static IList<INameValuePair> ToINameValuePairListWithUnfriendlyNames(this Type enumeration)
        {
            if (enumeration.GetTypeInfo().BaseType != typeof (Enum))
            {
                throw new ArgumentException(Strings.Validation.enumerationMustBeAnEnum);
            }
            var ret =
                Enum.GetNames(enumeration)
                    .Select(e => (INameValuePair) new NameValuePair(e, ((int) Enum.Parse(enumeration, e)).ToString()))
                    .ToList();
            return ret;
        }

#if !PCL && !DNXCORE50
        public static IEnumerable<INameValuePair> AsEnumerable(this NameValueCollection nvc)
        {
            var result = new List<INameValuePair>();
            for (var i = 0; i < nvc.Count; i++)
            {
                result.Add(new NameValuePair(nvc.Keys[i], nvc[i]));
            }
            return result.AsEnumerable();
        }
#endif
    }
}