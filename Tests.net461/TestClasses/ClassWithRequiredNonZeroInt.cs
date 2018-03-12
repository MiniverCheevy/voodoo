using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithRequiredNonZeroInt
    {
        [RequiredNonZeroInt]
        public int Number { get; set; }
    }
}