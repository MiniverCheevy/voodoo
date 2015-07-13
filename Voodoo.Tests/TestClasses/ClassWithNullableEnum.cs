using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithNullableEnum
    {
#if (!PCL)
        [EnumIsRequired]
#endif
        public TestEnum? TestEnum { get; set; }
    }
}