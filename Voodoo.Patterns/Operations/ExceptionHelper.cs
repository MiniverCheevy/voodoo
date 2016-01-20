using System;
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
               

                var builder = new StringBuilder();
                builder.AppendFormat("Details for '{0}' exception:", ex.Message);
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
                builder.AppendLine("Assert.AreEqual(true, response.IsOk);");



                if (VoodooGlobalConfiguration.ErrorDetailLoggingMethodology == ErrorDetailLoggingMethodology.LogInExceptionData)
                    ex.Data["Test"] = builder.ToString();

                LogManager.Logger.Log(ex);

                if (VoodooGlobalConfiguration.ErrorDetailLoggingMethodology == ErrorDetailLoggingMethodology.LogAsSecondException)
                    LogManager.Logger.Log(builder.ToString());

            }
        }
    }
}