using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithRequiredInt
    {
        [RequiredInt]
        public int Number { get; set; }
    }
}