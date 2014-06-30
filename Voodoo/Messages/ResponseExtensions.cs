using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Messages
{
    public static class ResponseExtensions
    {
        public static ListResponse<T> ToListResponse<T>(this IEnumerable<T> items) where T : class, new()
        {
            var result = new ListResponse<T>();
            result.Data.AddRange(items);
            return result;
        }
    }
}