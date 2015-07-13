using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithInt
    {
#if (!PCL)
        [GreaterThanZeroIntegerIsRequired]
#endif
        public int Number { get; set; }
    }
}