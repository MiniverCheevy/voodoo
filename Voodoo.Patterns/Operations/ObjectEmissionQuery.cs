using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Voodoo.Infrastructure.Notations;
using Voodoo.Messages;

namespace Voodoo.Operations
{
    public class ObjectEmissionQuery : Query<object, TextResponse>
    {
        private const int MaxItemsInGraph = 1000;
        private readonly List<int> hashes = new List<int>();
        private readonly StringBuilder result = new StringBuilder();
        private int currentItemsInGraph;
        private int depth;

        public ObjectEmissionQuery(object request) : base(request)
        {
        }

        protected override TextResponse ProcessRequest()

        {
            if (request == null)
            {
                response.Message = "=null";
                response.IsOk = false;
                return response;
            }

            read(request);

            response.Text = "var request=" + result + ";";
            return response;
        }

        protected override void Validate()
        {
        }

        private string read(object element)
        {
            if (currentItemsInGraph > MaxItemsInGraph)
                return string.Empty;


            if (element == null || element is ValueType || element is string)
                writeInline(format(element));
            else
                readObject(element);

            return result.ToString().TrimEnd(',');
        }

        private void readObject(object element)
        {
            var objectType = element.GetType();

            writeNoPad(" new {0} {{", objectType.FixUpTypeName());
            depth++;
            hashes.Add(element.GetHashCode());
            var enumerableElement = element as IEnumerable;
            if (enumerableElement != null && !objectType.IsScalar())
                readEnumerable(enumerableElement);
            else
                readPropertiesFromObject(element);
            depth--;
            write("}},");
        }

        private void readPropertiesFromObject(object element)
        {
#if !PCL
            var members =
                element.GetType()
                    .GetMembers(BindingFlags.Public | BindingFlags.Instance)
                    .OrderBy(OrderProperties)
                    .ThenBy(c => c.Name)
                    .ToArray();
#else
            var members =
                  element.GetType().GetTypeInfo().GetAllProperties()
                    .OrderBy(OrderProperties)
                    .ThenBy(c => c.Name)
                    .ToArray();
#endif
            foreach (var memberInfo in members)
            {
#if PCL
                var propertyInfo = memberInfo;
#else
                var propertyInfo = memberInfo as PropertyInfo;
#endif
                if (propertyInfo == null || !propertyInfo.CanWrite)
                    continue;

                var type = propertyInfo.PropertyType;
                if (propertyInfo.GetCustomAttributes(typeof(SecretAttribute),false).Any())
                    return;

                object value = null;
                try
                {
                    value = propertyInfo.GetValue(element, null);
                }
                catch
                {
                }
                if (type.IsScalar())
                {
                    write("{0}={1},", memberInfo.Name, format(value));
                }
                else if (value == null)
                {
                    write("{0}=null,", memberInfo.Name);
                }
                else
                {
                    if (alreadyTouched(value))
                    {
                        write("//{1} = new {0}() <-- bidirectional reference found",
                            element.GetType().FixUpTypeName(), memberInfo.Name);
                    }
                    else
                    {
                        writeInline("{0}=", memberInfo.Name);
                        readObject(value);
                    }
                }
            }
        }

        private void readEnumerable(IEnumerable enumerableElement)
        {
            var counter = 0;
            foreach (var item in enumerableElement)
            {
                counter++;
                if (counter > VoodooGlobalConfiguration.LogMaximumNumberOfItemsInCollection)
                    break;

                read(item);
                if (item != null && item.GetType().IsScalar())
                    writeNoPad(",");
            }
        }

        private int OrderProperties(MemberInfo arg)
        {
            if (arg is PropertyInfo)
            {
                var info = arg.To<PropertyInfo>();
                if (info.PropertyType.IsScalar())
                    return 1;
                if (info.PropertyType.IsEnumerable())
                    return 3;
                return 2;
            }
            return 10;
        }

        private bool alreadyTouched(object value)
        {
            if (value == null)
                return false;
            if (value.GetType().IsScalar())
                return false;
            var hash = value.GetHashCode();
            var wasTouched = hashes.Contains(hash);
            if (!wasTouched)
                hashes.Add(hash);
            return wasTouched;
        }

        private void write(string value, params object[] args)
        {
            var pad = depth*5;
            result.Append(new string(' ', pad));
            currentItemsInGraph++;

            if (args != null)
                value = string.Format(value, args);

            result.AppendLine(value);
        }

        private void writeNoPad(string value, params object[] args)
        {
            currentItemsInGraph++;

            if (args != null)
                value = string.Format(value, args);

            result.AppendLine(value);
        }

        private void writeInline(string value, params object[] args)
        {
            var pad = depth*5;
            result.Append(new string(' ', pad));
            if (args != null)
                value = string.Format(value, args);

            result.Append(value);
        }

        private void writeInlineNoPad(string value, params object[] args)
        {
            if (args != null)
                value = string.Format(value, args);

            result.Append(value);
        }

        private string format(object o)
        {
            if (o == null)
                return ("null");
            if (o is Enum)
            {
                var value = o.ToString();
                if (value == "0")
                    return string.Format("({0})0", o.GetType().Name);
                return string.Format("{0}.{1}", o.GetType().Name, o);
            }
            if (o is DateTime)
            {
                var date = o.To<DateTime>();
                return string.Format("new DateTime({0}, {1}, {2}, {3}, {4}, {5})", date.Year, date.Month, date.Day,
                    date.Hour, date.Minute, date.Millisecond);
            }
            if (o is decimal)
            {
                return string.Format("{0}m", o);
            }

            if (o is string)
                return string.Format("\"{0}\"", o);

            if (o is char && (char) o == '\0')
                return string.Empty;
            if (o is bool)
                return o.ToString().ToLower();
            if (o.GetType() == typeof (bool?) && o.To<bool?>().HasValue)
                return o.To<bool?>().ToString().ToLower();

            if (o is ValueType)
                return (o.ToString());

            return (string.Format("//cannot generate code for {0}", o.GetType().FixUpTypeName()));
        }
    }
}