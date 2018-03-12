using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithEnum
    {
        [EnumIsRequired]
        public TestEnum TestEnum { get; set; }
    }
}