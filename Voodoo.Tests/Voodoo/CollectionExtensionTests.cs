using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class CollectionExtensionTests
    {

        [TestMethod]
        public void ForEeach_Collection_EnumeratesProperly()
        {
            var items = new[] { "A", "B", "C" };
            var count = 0;
            items.ForEach( c => count++);
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void ContainsAny_Contained_ReturnsTrue()
        {
            var source = new[] {"A", "B", "C"};
            var target = new[] {"C", "D"};
            var result = source.ContainsAny(target);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ContainsAny_NotContained_ReturnsFalse()
        {
            var source = new[] {"A", "B", "C"};
            var target = new[] {"D", "E"};
            var result = source.ContainsAny(target);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ContainsAll_ContainsAll_ReturnsTrue()
        {
            var source = new[] {"A", "B", "C"};
            var target = new[] {"A", "B", "C"};
            var result = source.ContainsAll(target);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void ContainsAll_EmptyList_ReturnsFalse()
        {
            var source = new string[] {};
            var target = new[] { "A", "B", "C" };
            var result = source.ContainsAll(target);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ContainsAll_ContainsAllExcept1_ReturnsFalse()
        {
            var source = new[] {"A", "B", "C"};
            var target = new[] {"A", "B", "C", "D"};
            var result = source.ContainsAll(target);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ToArray_CollectionOfStrings_ConvertedToArray()
        {
            ICollection source = new[] {"A", "B", "C"};
            var converted = source.ToArray<string>();
            Assert.AreEqual(converted.GetType(), typeof (string[]));
        }

        [TestMethod]
        public void AddIfNotNull_NullObjet_NotAdded()
        {
            var list = new List<string>();
            string nullString = null;
            list.AddIfNotNull(nullString);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void AddIfNotNull_NotNullObjet_Added()
        {
            var list = new List<string>();
            var nonNullString = "string";
            list.AddIfNotNull(nonNullString);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void AddIfNotNullOrWhiteSpace_NullObjet_NotAdded()
        {
            var list = new List<string>();
            string nullString = null;
            list.AddIfNotNullOrWhiteSpace(nullString);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void AddIfNotNullOrWhiteSpace_WhitepsaceObjet_NotAdded()
        {
            var list = new List<string>();
            var spacedString = "    ";
            list.AddIfNotNullOrWhiteSpace(spacedString);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void AddIfNotNullOrWhiteSpace_NotNullObjet_NotAdded()
        {
            var list = new List<string>();
            var stringString = "string";
            list.AddIfNotNull(stringString);
            Assert.AreEqual(1, list.Count);
        }
    }
}