using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithGuidAsString
    {
#if !PCL
        [RequiredGuid]
#endif
        public string Guid { get; set; }
    }
}