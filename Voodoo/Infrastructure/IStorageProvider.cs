using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Infrastructure
{
    public interface IStorageProvider
    {
        bool Exists { get; }
        void Flush();
        bool Contains(string key);
        T Get<T>(string key);
        void Remove(string key);
        void Put<T>(string key, T value, int? durationInMinutes = null);
    }
}