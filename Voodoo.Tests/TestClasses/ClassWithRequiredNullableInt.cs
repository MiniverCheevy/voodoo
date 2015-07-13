using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithRequiredNullableInt
    {
#if (!PCL)
        [RequiredInt]
#endif
        public int? Number { get; set; }
    }
}