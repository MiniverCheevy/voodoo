using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Infrastructure.Notations;

namespace Voodoo.Logging
{
    [Unfinished]
    public interface ILog
    {
        long Id { get; set; }
        string Action { get; set; }
        string Exception { get; set; }
        string IP { get; set; }
        string MethodCall { get; set; }
        string Parameters { get; set; }
        string Result { get; set; }
        DateTime Time { get; set; }
        string Category { get; set; }
        string UserName { get; set; }
        string Url { get; set; }
        string SessionId { get; set; }
        string Referrer { get; set; }
        string Browser { get; set; }
        string UserAgent { get; set; }
    }

    [Unfinished]
    public class DefaultLog : ILog
    {
        public string Action { get; set; }
        public string Exception { get; set; }
        public long Id { get; set; }
        public string IP { get; set; }
        public string MethodCall { get; set; }
        public string Parameters { get; set; }
        public string Result { get; set; }
        public DateTime Time { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Category { get; set; }
        public string SessionId { get; set; }
        public string Referrer { get; set; }
        public string Browser { get; set; }
        public string UserAgent { get; set; }
    }
}