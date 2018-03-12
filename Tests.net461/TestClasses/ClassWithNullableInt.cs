using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithNullableInt
    {
        [RequiredNonZeroInt]
        public int? Number { get; set; }
    }
}