using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo.Logging;

namespace Voodoo
{
    public class ModelHelper
    {
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
#if !PCL && !DNXCORE50
        public static T GetAttributeFromEnumMember<T>(object enumMember)
            where T:Attribute
        {
            try
            {
                var type = typeof(T);
                var memberInfos = type.GetMember(enumMember.To<string>());
                if (memberInfos.Any())
                {
                    var attribute = memberInfos.First().GetCustomAttributes(type, true).FirstOrDefault();
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
