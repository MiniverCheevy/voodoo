using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voodoo.Infrastructure;
using Voodoo.Logging;

namespace Voodoo.Operations
{
    internal class ExceptionHelper
    {
        internal static void HandleException(Exception ex, Type type, object request)
        {
            if (!(ex is LogicException))
            {
                LogManager.Logger.Log(ex);

                var builder = new StringBuilder();
                builder.AppendFormat("Details for '{0}' exception:", ex.Message);
                builder.AppendLine(string.Empty);
                builder.Append(request.ToDebugString());
                builder.AppendLine(string.Empty);
                builder.AppendLine("Code to reproduce error:");
                builder.AppendLine(string.Empty);

                builder.Append(request.ToCode());
                builder.AppendLine(string.Empty);
                var thisType = type.FixUpTypeName();
                if (thisType.ToLower().Contains("async"))
                {
                    builder.AppendFormat("var response = await new {0}(request).ExecuteAsync();", thisType);
                }
                else 
                {
                    builder.AppendFormat("var response = new {0}(request).Execute();", thisType);
                }
                builder.AppendLine(string.Empty);
                builder.AppendLine(
                    "Assert.AreEqual(response.Message, \"Some people just want to watch the world burn.\");");
                builder.AppendLine("Assert.AreEqual(true, response.IsOk);");

                LogManager.Log(builder.ToString());
            }
        }
    }
}