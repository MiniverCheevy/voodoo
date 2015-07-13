using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithEnum
    {
#if !PCL
        [EnumIsRequired]
#endif
        public TestEnum TestEnum { get; set; }
    }
}