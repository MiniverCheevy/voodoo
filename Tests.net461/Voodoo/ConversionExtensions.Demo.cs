using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class ConversionExtensions
    {
        [TestMethod]
        public void ToAndAs()
        {
            var foo = new Foo {AProperty = "A"};
            var bar = new Bar {AProperty = "B"};

            var toInterface = foo.To<IHaveAProperty>();
            Assert.AreEqual(foo, toInterface);

            var cantCast = bar.To<Foo>();
            Assert.IsNotNull(bar);
            Assert.AreNotEqual<object>(bar, cantCast);

            decimal? number = null;
            Assert.AreEqual(null, number.As<decimal?>());
            Assert.AreEqual(0, number.As<decimal>());
        }

        public interface IHaveAProperty
        {
            string AProperty { get; set; }
        }

        public class Foo : IHaveAProperty
        {
            public string AProperty { get; set; }
        }

        public class Bar : IHaveAProperty
        {
            public string AProperty { get; set; }
        }
    }
}