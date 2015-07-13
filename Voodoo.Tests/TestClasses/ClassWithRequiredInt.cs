using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithRequiredInt
    {
#if !PCL
        [RequiredInt]
#endif
        public int Number { get; set; }
    }
}