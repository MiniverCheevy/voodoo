using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithRequiredNonZeroNullableInt
    {
        [RequiredNonZeroInt]
        public int? Number { get; set; }
    }
}