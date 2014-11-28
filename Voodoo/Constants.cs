using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo
{
    [Obsolete("Use Voodoo.Strings instead")]
    public class Constants
    {
        [Obsolete("Use Voodoo.Strings.SortDirection instead")]
        public struct SortDirection
        {
            public static readonly string Descending = "DESC";
            public static readonly string Ascending = "ASC";
        }
    }
}