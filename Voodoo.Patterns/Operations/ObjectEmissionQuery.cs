using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Voodoo.Infrastructure.Notations;
using Voodoo.Logging;
using Voodoo.Messages;

namespace Voodoo.Operations
{
    public class ObjectEmissionQuery : Query<ObjectEmissionRequest, TextResponse>
    {
        private const int MaxItemsInGraph = 1000;
        private readonly List<int> hashes = new List<int>();
        private readonly StringBuilder result = new StringBuilder();
        private int currentItemsInGraph;
        private int depth;

        public ObjectEmissionQuery(ObjectEmissionRequest request) : base(request)
        {
        }

        protected override TextResponse ProcessRequest()

        {
            if (request.Source == null)
            {
                response.Message = "=null";
                response.IsOk = false;
                return response;
            }

            read(request.Source);

            if (string.IsNullOrWhiteSpace(request.Name))
                request.Name = "request";

            response.Text = $"var {request.Name}=" + result + ";";
            return response;
        }

        protected override void Validate()
        {
        }

        private string read(object element)
        {
            if (currentItemsInGraph > MaxItemsInGraph)
                return string.Empty;

            if (element == null && !request.IncludeNull)
                return string.Empty;

            if ( element is ValueType || element is string)
                writeInline(format(element));
            else
                readObject(element);

            return result.ToString();
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
            write("}}");
        }

        private void readPropertiesFromObject(object element)
        {
            var members =
                element.GetType().GetTypeInfo().GetAllProperties()
                    .OrderBy(OrderProperties)
                    .ThenBy(c => c.Name)
                    .ToArray();

            foreach (var memberInfo in members)
            {
                var propertyInfo = memberInfo as PropertyInfo;
                if (propertyInfo == null || !propertyInfo.CanWrite)
                    continue;

                var type = propertyInfo.PropertyType;
                if (propertyInfo.GetCustomAttributes(typeof(SecretAttribute), false).Any())
                    continue;

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
                        write(",");
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
        private string format(string value, params object[] args)
        {
            try
            {
                return string.Format(value, args);
            }
            catch (Exception ex)
            {
                ex.Data.Add("FormatString", value);
                ex.Data.Add("FormatValues", args);
                LogManager.Log(ex);
                return "Failed To Format:" + value + String.Join(",", args) +" " +ex.Message;
            }
            

        }
        private void write(string value, params object[] args)
        {
            var pad = depth * 5;
            result.Append(new string(' ', pad));
            currentItemsInGraph++;

            if (args != null)
                value = format(value, args);
            if (value == "}")
                result.Append(value);
            else
                result.AppendLine(value);
        }

        private void writeNoPad(string value, params object[] args)
        {
            currentItemsInGraph++;

            if (args != null)
                value = format(value, args);

            if (value == "}")
                result.Append(value);
            else
                result.AppendLine(value);
        }

        private void writeInline(string value, params object[] args)
        {
            var pad = depth * 5;
            result.Append(new string(' ', pad));
            if (args != null)
                value = format(value, args);

            result.Append(value);
        }

        private void writeInlineNoPad(string value, params object[] args)
        {
            if (args != null)
                value = format(value, args);

            result.Append(value);
        }

        private string format(object o)
        {
            if (o == null)
                return ("null");
            else if (o is Enum)
            {
                var value = o.ToString();
                if (value == "0")
                    return format("({0})0", o.GetType().Name);
                return format("{0}.{1}", o.GetType().Name, o);
            }
            else if (o is Guid)
                return $"Guid.Parse(\"{o.ToString()}\")";
            else if (o is DateTime)
            {
                return $"DateTime.Parse(\"{o}\")";
            }
            else if (o is DateTimeOffset)
            {
                return $"DateTimeOffset.Parse(\"{o}\")";
            }
            else if (o is decimal)
                return format("{0}m", o);
            else if (o is string)
                return format("\"{0}\"", o);
            else if (o is char && (char) o == '\0')
                return "(char)0";
            else if (o is char )
                return $"'{o}'";
            else if (o is bool)
                return o.ToString().ToLower();
            else if (o.GetType() == typeof(bool?) && o.To<bool?>().HasValue)
                return o.To<bool?>().ToString().ToLower();
            else if (o is ValueType)
                return (o.ToString());
            else
                return (format("//cannot generate code for {0}", o.GetType().FixUpTypeName()));
        }
    }
}