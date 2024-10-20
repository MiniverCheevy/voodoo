using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo
{
    
    public class NameIdExtensionTests
    {
        [Fact]
        public void ToINameIdPairList_ValueIsEnum_ReturnesList()
        {
            var list = typeof(TestEnum).ToINameIdPairList();
            Debug.WriteLine(list.Count());
            list.Count().Should().Be(3);
        }

        [Fact]
        public void ToINameIdPairListWithUnfriendlyNames_ValueIsEnum_ReturnesList()
        {
            var list = typeof(TestEnum).ToINameIdPairListWithUnfriendlyNames();
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void ToDictionary_ExistingList_IsOk()
        {
            var list = typeof(TestEnum).ToINameIdPairListWithUnfriendlyNames();
            var dictionary = list.ToDictionary();
        }
    }
}