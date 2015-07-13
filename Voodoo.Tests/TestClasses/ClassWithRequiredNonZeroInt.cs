using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithRequiredNonZeroInt
    {
#if (!PCL)
        [RequiredNonZeroInt]
#endif
        public int Number { get; set; }
    }
}