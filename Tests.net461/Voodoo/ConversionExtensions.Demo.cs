using Xunit;

namespace Voodoo.Tests.Voodoo
{
    
    public class ConversionExtensions
    {
        [Fact]
        public void ToAndAs()
        {
            var foo = new Foo {AProperty = "A"};
            var bar = new Bar {AProperty = "B"};

            var toInterface = foo.To<IHaveAProperty>();
            Assert.Equal(foo, toInterface);

            var cantCast = bar.To<Foo>();
            Assert.IsNotNull(bar);
            Assert.AreNotEqual<object>(bar, cantCast);

            decimal? number = null;
            Assert.Null(number.As<decimal?>());
            Assert.Equal(0, number.As<decimal>());
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