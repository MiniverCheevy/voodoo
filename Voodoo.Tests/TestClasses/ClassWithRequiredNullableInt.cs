using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithRequiredNullableInt
    {
        [RequiredInt]
        public int? Number { get; set; }
    }
}