using System.Linq;
using System.Reflection;
using Xunit;


namespace Voodoo.Tests.Voodoo
{
    public abstract class TestBaseClass<T>
        where T : class, new()
    {
    }

    public class MessageClass
    {
    }

    public class TestSubClass : TestBaseClass<MessageClass>
    {
    }

    
    public class ReflectionHierarchyTests
    {
        [Fact]
        public void ProveSubClassInheritsFromBase()
        {
            var baseType =
                typeof(MessageClass).GetTypeInfo().Assembly.DefinedTypes.First(c => c.Name.StartsWith("TestBaseClass"));
            var subType = new TestSubClass().GetType().GetTypeInfo();

            //Assert.Equal failed. Expected:<Voodoo.Tests.Voodoo.TestBaseClass`1[T]>. Actual:<Voodoo.Tests.Voodoo.TestBaseClass`1[Voodoo.Tests.Voodoo.MessageClass]>. 

            Assert.AreNotEqual(baseType, subType.BaseType.GetTypeInfo());
        }
    }
}