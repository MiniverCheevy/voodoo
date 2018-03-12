using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class NameIdExtensionTests
    {
        [TestMethod]
        public void ToINameIdPairList_ValueIsEnum_ReturnesList()
        {
            var list = typeof(TestEnum).ToINameIdPairList();
            Debug.WriteLine(list.Count());
            list.Count().Should().Be(3);
        }

        [TestMethod]
        public void ToINameIdPairListWithUnfriendlyNames_ValueIsEnum_ReturnesList()
        {
            var list = typeof(TestEnum).ToINameIdPairListWithUnfriendlyNames();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void ToDictionary_ExistingList_IsOk()
        {
            var list = typeof(TestEnum).ToINameIdPairListWithUnfriendlyNames();
            var dictionary = list.ToDictionary();
        }
    }
}