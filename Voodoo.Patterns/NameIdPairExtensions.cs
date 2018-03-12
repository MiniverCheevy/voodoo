using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Messages;

namespace Voodoo
{
    public static class NameIdPairExtensions
    {
        public static IList<INameIdPair> ToINameIdPairList(this Type enumeration)
        {
            if (enumeration.GetTypeInfo().BaseType != typeof(Enum))
            {
                throw new ArgumentException(Strings.Validation.enumerationMustBeAnEnum);
            }
            var result =
                Enum.GetNames(enumeration)
                    .Select(
                        e =>
                            (INameIdPair)
                            new NameIdPair(
                                Enum.Parse(enumeration, e).ToFriendlyString(), ((int) Enum.Parse(enumeration, e))))
                    .ToList();
            return result;
        }

        public static IList<INameIdPair> ToINameIdPairListWithUnfriendlyNames(this Type enumeration)
        {
            if (enumeration.GetTypeInfo().BaseType != typeof(Enum))
            {
                throw new ArgumentException(Strings.Validation.enumerationMustBeAnEnum);
            }
            var ret =
                Enum.GetNames(enumeration)
                    .Select(e => (INameIdPair) new NameIdPair(e, ((int) Enum.Parse(enumeration, e))))
                    .ToList();
            return ret;
        }

        public static Dictionary<int, string> ToDictionary(this IList<INameIdPair> list,
            bool throwForDuplicateKeys = false)
        {
            var result = new Dictionary<int, string>();
            foreach (var item in list)
            {
                if ((result.ContainsKey(item.Id) && throwForDuplicateKeys) || !result.ContainsKey(item.Id))
                    result.Add(item.Id, item.Name);
            }
            return result;
        }
    }
}