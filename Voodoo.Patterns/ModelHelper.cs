using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo.Logging;

namespace Voodoo
{
    public static class ModelHelper
    {
        public static string ExtractUserNameFromDomainNameOrEmailAddress(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            var slashPosition = name.IndexOf('\\');
            var atPosition = name.IndexOf('@');
            if (slashPosition > -1)
                name = name.Substring(slashPosition+1);
            if (atPosition > -1)
                name = name.Substring(0, atPosition);

            return name;

        }
        public static string Truncate(this string s, int maxLength)
        {
            if (s == null)
                return string.Empty;
            return (s.Length > maxLength) ? s.Remove(maxLength) : s;
        }
        public static string FormatPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone) || phone.Length != 10)
                return phone.To<string>();

            return $"({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 4)}";
        }
        public static string UnformatPhone(string phone)
        {
            return new string(phone.To<string>().ToArray<char>().Where(char.IsDigit).ToArray());
        }
#if !PCL && !NETCOREAPP1_0
        public static T GetAttributeFromEnumMember<T>(object enumMember)
            where T : Attribute
        {
            try
            {
                var attributeType = typeof(T);
                var enumType = enumMember.GetType();
                var memberInfos = enumType.GetMember(enumMember.To<string>());
                if (memberInfos.Any())
                {
                    var attribute = memberInfos.First()
                        .GetCustomAttributes(true)
                        .FirstOrDefault(c => c.GetType() == attributeType);
                    if (attribute == null)
                        return null;

                    return attribute.To<T>();
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
            }
            return null;
        }
#endif
    }
}
