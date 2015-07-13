using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithRequiredNonZeroNullableInt
    {
#if (!PCL)
        [RequiredNonZeroInt]
#endif
        public int? Number { get; set; }
    }
}