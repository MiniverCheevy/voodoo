using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Voodoo.Logging
{
    public class FallbackLogger : ILogger
    {
        private static readonly object locker = new object();

        public void Log(Exception ex)
        {
            Log(ex.ToString(), null);
        }

        public void Log(string log)
        {
            Log(log, null);
        }

        public void Log(string log, string logFilePath)
        {
            var appName = getAppName();
            try
            {
                var path = getLogFilePath(logFilePath);

                deleteFileIfNeeded(path);

                var text = string.Concat(DateTime.Now.ToLongDateString(), " ", DateTime.Now.ToLongTimeString(),
                    Environment.NewLine, log, Environment.NewLine,
                    "**********************************************************", Environment.NewLine);

                lock (locker)
                {
                    File.AppendAllText(path, text);
                }
            }
            catch (Exception ex)
            {
                handleFileWriteFailure(log, ex, appName);
            }
        }

        private static void handleFileWriteFailure(string log, Exception ex, string appName)
        {
            Debug.WriteLine("Error in Fallback Logger: " + ex);

            var source = appName ?? "Application";
            const string logName = "Application";
            var eventName = "Fallback Logger" + ex;

            try
            {
                if (!EventLog.SourceExists(source))
                    EventLog.CreateEventSource(source, logName);
            }
            catch
            {
                source = "ASP.NET 4.0.30319.0";

                var command =
                    string.Format(
                        "eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO {0} /D \"Event Source Created\"", appName);
                var message =
                    string.Concat(
                        "Event source does not exist for this application, you can set v:appName in the config file to customize it and/or run the following command ",
                        Environment.NewLine, command, Environment.NewLine,
                        "You may have to change the /ID parameter if it already exists");
                EventLog.WriteEntry(source, message, EventLogEntryType.Warning);
            }


            EventLog.WriteEntry(source, eventName, EventLogEntryType.Warning);

            EventLog.WriteEntry(source, log, EventLogEntryType.Error);
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
            var configuredPath = ConfigurationManager.AppSettings["v:logFilePath"];
            if (string.IsNullOrEmpty(logFilePath))
                logFilePath = configuredPath;


            var today = DateTime.Now.DayOfWeek.ToString();

            if (string.IsNullOrEmpty(logFilePath))
            {
                logFilePath = HttpContext.Current != null ? HttpContext.Current.Server.MapPath("~") : @"c:\Logs\";
            }
            var fileName = string.Format("log.{0}.txt", today);
            var path = Path.Combine(logFilePath, fileName);
            return path;
        }

        private string getAppName()
        {
            var appName = string.Empty;
            try
            {
                appName = ConfigurationManager.AppSettings["v:appName"];
                if (string.IsNullOrEmpty(appName))
                {
                    var assembly = Assembly.GetCallingAssembly() == null
                        ? AppDomain.CurrentDomain.FriendlyName
                        : Assembly.GetCallingAssembly().FullName;
                    appName = assembly.Split(',')[0];
                }
            }
            catch
            {
            }
            return appName;
        }
    }
}