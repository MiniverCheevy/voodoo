using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithInt
    {
        [RequiredNonZeroInt]
        public int Number { get; set; }
    }
}