using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Voodoo;
using Voodoo.Messages;

namespace Voodoo.Operations
{
    //http://stackoverflow.com/questions/852181/c-printing-all-properties-of-an-object
    public class ObjectStringificationQuery : Query<object, TextResponse>
    {
        private readonly List<int> hashes;
        private readonly int padding;
        private readonly StringBuilder result;
        private int currentItemsInGraph;
        private int depth;
        private int maxItemsInGraph = 1000;

        public ObjectStringificationQuery(object request) : base(request)
        {
            padding = 5;
            result = new StringBuilder();
            hashes = new List<int>();
        }

        protected override TextResponse ProcessRequest()
        {
            response.Text = read(request);
            return response;
        }
	protected override void Validate()
        {
            
        }
        private string read(object element)
        {
            if (currentItemsInGraph > maxItemsInGraph)
                return string.Empty;


            if (element == null || element is ValueType || element is string)
            {
                write(format(element));
            }
            else
            {
                var objectType = element.GetType();
                if (!typeof (IEnumerable).IsAssignableFrom(objectType))
                {
                    write("{{{0}}}", objectType.FullName);
                    hashes.Add(element.GetHashCode());
                    depth++;
                }

                var enumerableElement = element as IEnumerable;
                if (enumerableElement != null && ! objectType.IsScalar())
                {
                    var counter = 0;
                    foreach (var item in enumerableElement)
                    {
                        counter++;
                        if (counter > VoodooGlobalConfiguration.LogMaximumNumberOfItemsInCollection)
                            break;

                        if (item is IEnumerable && !(item is string))
                        {
                            depth++;
                            read(item);
                            depth--;
                        }
                        else
                        {
                            if (!alreadyTouched(item))
                                read(item);
                            else
                                write("{{{0}}} <-- bidirectional reference found", item.GetType().FullName);
                        }
                    }
                }
                else
                {
                    var members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)
                        .OrderBy(OrderProperties)
                        .ThenBy(c=>c.Name)
                        .ToArray();
                    foreach (var memberInfo in members)
                    {
                        var fieldInfo = memberInfo as FieldInfo;
                        var propertyInfo = memberInfo as PropertyInfo;

                        if (fieldInfo == null && propertyInfo == null)
                            continue;

                        var type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                        object value = null;
                        try
                        {
                            value = fieldInfo != null
                                ? fieldInfo.GetValue(element)
                                : propertyInfo.GetValue(element, null);
                        }
                        catch (Exception ex)
                        {
                            value = ex.Message;
                        }
                        if (type.IsValueType || type == typeof (string))
                        {
                            write("{0}: {1}", memberInfo.Name, format(value));
                        }
                        else
                        {
                            var isEnumerable = typeof (IEnumerable).IsAssignableFrom(type);
                            var isScalar = type.IsScalar();
                            write("{0}: {1}", memberInfo.Name, isEnumerable ? "..." : "{ }");

                            var alreadyTouched = !isScalar && !isEnumerable && this.alreadyTouched(value);
                            depth++;
                            if (!alreadyTouched)
                                read(value);
                            else
                                write("{{{0}}} <-- bidirectional reference found", value.GetType().FullName);
                            depth--;
                        }
                    }
                }

                if (!typeof (IEnumerable).IsAssignableFrom(objectType))
                {
                    depth--;
                }
            }

            return result.ToString();
        }

        private int OrderProperties(MemberInfo arg)
        {
            if (arg is PropertyInfo)
            {
                var info = arg.To<PropertyInfo>();
                if (info.PropertyType.IsScalar())
                    return 1;
                else if (info.PropertyType.IsEnumerable())
                    return 3;
                else
                    return 2;
            }
            else
            {
                return 10;
            }
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
            currentItemsInGraph++;
            var space = new string(' ', depth*padding);

            if (args != null)
                value = string.Format(value, args);

            result.AppendLine(space + value);
        }

        private string format(object o)
        {
            if (o == null)
                return ("null");

            if (o is DateTime)
                return (((DateTime) o).ToShortDateString());

            if (o is string)
                return string.Format("\"{0}\"", o);

            if (o is char && (char) o == '\0')
                return string.Empty;

            if (o is ValueType)
                return (o.ToString());

            if (o is IEnumerable)
                return ("...");

            return ("{ }");
        }
    }
}