using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithGuidAsString
    {
        [RequiredGuid]
        public string Guid { get; set; }
    }
}