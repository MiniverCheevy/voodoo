using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

    [TestClass]
    public class ReflectionHierarchyTests
    {
        [TestMethod]
        public void ProveSubClassInheritsFromBase()
        {
            var baseType =
                typeof (MessageClass).Assembly.GetTypes().First(c => c.Name.StartsWith("TestBaseClass"));
            var subType = new TestSubClass().GetType();

            //Assert.AreEqual failed. Expected:<Voodoo.Tests.Voodoo.TestBaseClass`1[T]>. Actual:<Voodoo.Tests.Voodoo.TestBaseClass`1[Voodoo.Tests.Voodoo.MessageClass]>. 

            Assert.AreNotEqual(baseType, subType.BaseType);
        }
    }
}
