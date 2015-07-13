using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithNullableInt
    {
#if (!PCL)
        [GreaterThanZeroIntegerIsRequired]
#endif
        public int? Number { get; set; }
    }
}