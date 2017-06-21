using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Voodoo.Infrastructure;
using Voodoo.Infrastructure.Notations;
#if (!PCL)
namespace Voodoo.Logging
{
    public class FallbackLogger : ILogger
    {
        private static readonly object locker = new object();

        public void Log(Exception ex)
        {
            var log = new StringBuilder();
            log.Append(ex.ToString());
            foreach (var i in ex.Data)
            {
                if (i is DictionaryEntry)
                {
                    var item = i.To<DictionaryEntry>();
                    log.AppendLine("");
                    log.AppendLine(item.Key.To<string>());
                    log.AppendLine(item.Value.To<string>());

                }
            }

            Log(ex.ToString(), null);
        }

        public void Log(string log)
        {
            Log(log, null);
        }

        public void Log(string log, string logFilePath)
        {
            var path = string.Empty;
            var appName = getAppName();
            try
            {
                path = getLogFilePath(logFilePath);

                deleteFileIfNeeded(path);

                var text = string.Concat(DateTime.Now.ToString("F"),
                    Environment.NewLine, log, Environment.NewLine,
                    "**********************************************************", Environment.NewLine);
#if !NETCOREAPP1_0
                lock (locker)
                {
#endif
                    File.AppendAllText(path, text);
#if !NETCOREAPP1_0
                }
#endif
            }
            catch (Exception ex)
            {
                handleFileWriteFailure(log, ex, appName, path);
            }
        }

        private static void handleFileWriteFailure(string actualError, Exception ex, string appName, string path)
        {

#if !NETCOREAPP1_0
            var failedToWriteMessage = "Fallback Logger Failed to write log file: " + path;
            var source = appName ?? "Application";
            const string logName = "Application";

            try
            {
                if (!EventLog.SourceExists(source))
                    EventLog.CreateEventSource(source, logName);
            }
            catch
            {
                source = "ASP.NET 4.0.30319.0";

                var command =
                    $"eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO {appName} /D \"Event Source Created\"";
                var eventSourceDoesNotExistMessage =
                    string.Concat(
                        "Event source does not exist for this application, you can set v:appName in the config file to customize it and/or run the following command ",
                        Environment.NewLine, command, Environment.NewLine,
                        "You may have to change the /ID parameter if it already exists");
                EventLog.WriteEntry(source, eventSourceDoesNotExistMessage, EventLogEntryType.Warning);
            }

            var actualMessage = $"{actualError} {ex}";
            if (actualMessage.Length > 32000)
                actualMessage = actualMessage.Substring(0, 32000);
            EventLog.WriteEntry(source, failedToWriteMessage, EventLogEntryType.Warning);

            EventLog.WriteEntry(source, actualMessage, EventLogEntryType.Error);
#endif
        }

        private static void deleteFileIfNeeded(string path)
        {
            if (File.Exists(path))
            {
                var now = DateTime.Now;
                var lastWrite = File.GetLastWriteTime(path);
                if (lastWrite.Year == now.Year && lastWrite.Month == now.Month && lastWrite.Day == now.Day)
                {
                }
                else
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                    File.Delete(path);
                }
            }
        }

        private static string getLogFilePath(string logFilePath)
        {
            var configuredPath = VoodooGlobalConfiguration.LogFilePath;
            if (string.IsNullOrEmpty(logFilePath))
                logFilePath = configuredPath;


            var today = DateTime.Now.DayOfWeek.ToString();

            if (string.IsNullOrEmpty(logFilePath))
            {
                logFilePath = IoNic.IsWebHosted ? IoNic.GetApplicationRootDirectory() : @"c:\Logs";
            }
            var fileName = $"log.{today}.txt";
            var path = Path.Combine(logFilePath, fileName);
            return path;
        }

        private string getAppName()
        {
            var appName = string.Empty;
            try
            {
                appName = VoodooGlobalConfiguration.ApplicationName;
                if (string.IsNullOrEmpty(appName))
                {
#if !NETCOREAPP1_0 && !PCL
                    var assembly = Assembly.GetCallingAssembly() == null
                        ? AppDomain.CurrentDomain.FriendlyName
                        : Assembly.GetCallingAssembly().FullName;
                    appName = assembly.Split(',')[0];
                    appName = "Unnamed App";
#endif
#if NETCOREAPP1_0
                     appName="Unnamed DotNetCoreApp";
#endif
#if PCL
                     appName="Unnamed PortableApp";
#endif

                }
            }
            catch
            {
                appName = "Unname App";
            }
            return appName;
        }
    }
}
#endif