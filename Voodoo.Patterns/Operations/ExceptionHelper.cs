using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Voodoo.Infrastructure;
using Voodoo.Logging;

namespace Voodoo.Operations
{
    public static class ExceptionHelper
    {
        public static void HandleException(Exception ex, Type type, object request)
        {
            if (!(ex is LogicException))
            {
                var builder = new StringBuilder();
                builder.AppendFormat("Details for '{0}' exception:", ex.Message);
                builder.AppendLine(string.Empty);
                builder.AppendLine("Code to reproduce error:");
                builder.AppendLine(string.Empty);

                builder.Append(request.ToCode());
                builder.AppendLine(string.Empty);
                var thisType = type.FixUpTypeName();
                builder.AppendFormat(
                    thisType.ToLower().Contains("async")
                        ? "var response = await new {0}(request).ExecuteAsync();"
                        : "var response = new {0}(request).Execute();", thisType);
                builder.AppendLine(string.Empty);
                builder.AppendLine("Assert.AreEqual(true, response.IsOk);");

                appendDetails(ex);

                if (VoodooGlobalConfiguration.ErrorDetailLoggingMethodology ==
                    ErrorDetailLoggingMethodology.LogInExceptionData)
                    ex.Data["Test"] = builder.ToString();

                LogManager.Log(ex);

                if (VoodooGlobalConfiguration.ErrorDetailLoggingMethodology !=
                    ErrorDetailLoggingMethodology.LogAsSecondException) return;

                foreach (var item in ex.Data.Keys)
                {
                    try
                    {
                        builder.AppendLine();
                        builder.AppendLine($"{item} {ex.Data[item].ToDebugString()}");
                    }
                    catch (Exception e)
                    {
                        builder.AppendLine($"Failed to stringify details for {item}, {e.Message}");
                    }
                }
                LogManager.Log(builder.ToString(), "Error", LogLevels.Error);
            }
        }

        private static void appendDetails(Exception ex)
        {
            var ignored = new List<string>
            {
                "Message",
                "Data",
                "InnerException",
                "TargetSite",
                "StackTrace",
                "HelpLink",
                "Source",
                "HResult"
            };
            var exception = ex;

            while (exception != null)
            {
                var type = exception.GetType();
                foreach (var prop in getProperties(type))
                {
                    var key = $"{type.Name}.{prop.Name}";
                    try
                    {
                        if (!ignored.Contains(prop.Name) && !ex.Data.Contains(key))
                        {
                            var value = getValue(prop, exception);
                            ex.Data.Add(key, value.ToDebugString());
                        }
                    }
                    catch (Exception exp)
                    {
                        ex.Data.Add(key, $"failed to read exception details : {exp.Message}");
                    }
                }
                exception = exception.InnerException;
            }
        }

        private static object getValue(PropertyInfo prop, Exception ex)
        {
            return prop.GetValue(ex);
        }

        private static IEnumerable<PropertyInfo> getProperties(Type type)
        {
            return type.GetProperties();
        }
    }
}