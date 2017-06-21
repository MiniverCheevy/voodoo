using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
#if (!PCL)
    public class ClassWithDoubleProperties
    {
        public DateTime? Date1 { get; set; }

        [GreaterThan("Date1")]

        public DateTime? Date2 { get; set; }

        public int? Int1 { get; set; }

        [GreaterThan("Int1")]
        public int? Int2 { get; set; }

        public decimal? Decimal1 { get; set; }

        [GreaterThan("Decimal1")]
        public decimal? Decimal2 { get; set; }
    }
#endif
}
