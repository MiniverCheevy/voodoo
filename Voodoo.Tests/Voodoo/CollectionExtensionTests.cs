using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Voodoo.Tests.Voodoo
{
    
    public class CollectionExtensionTests
    {

        [Fact]
        public void ForEeach_Collection_EnumeratesProperly()
        {
            var items = new[] { "A", "B", "C" };
            var count = 0;
            items.ForEach( c => count++);
            Assert.Equal(3, count);
        }

        [Fact]
        public void ContainsAny_Contained_ReturnsTrue()
        {
            var source = new[] {"A", "B", "C"};
            var target = new[] {"C", "D"};
            var result = source.ContainsAny(target);
            Assert.Equal(true, result);
        }

        [Fact]
        public void ContainsAny_NotContained_ReturnsFalse()
        {
            var source = new[] {"A", "B", "C"};
            var target = new[] {"D", "E"};
            var result = source.ContainsAny(target);
            Assert.Equal(false, result);
        }

        [Fact]
        public void ContainsAll_ContainsAll_ReturnsTrue()
        {
            var source = new[] {"A", "B", "C"};
            var target = new[] {"A", "B", "C"};
            var result = source.ContainsAll(target);
            Assert.Equal(true, result);
        }
        [Fact]
        public void ContainsAll_EmptyList_ReturnsFalse()
        {
            var source = new string[] {};
            var target = new[] { "A", "B", "C" };
            var result = source.ContainsAll(target);
            Assert.Equal(false, result);
        }

        [Fact]
        public void ContainsAll_ContainsAllExcept1_ReturnsFalse()
        {
            var source = new[] {"A", "B", "C"};
            var target = new[] {"A", "B", "C", "D"};
            var result = source.ContainsAll(target);
            Assert.Equal(false, result);
        }

        [Fact]
        public void ToArray_CollectionOfStrings_ConvertedToArray()
        {
            ICollection source = new[] {"A", "B", "C"};
            var converted = source.ToArray<string>();
            Assert.Equal(converted.GetType(), typeof (string[]));
        }

        [Fact]
        public void AddIfNotNull_NullObjet_NotAdded()
        {
            var list = new List<string>();
            string nullString = null;
            list.AddIfNotNull(nullString);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void AddIfNotNull_NotNullObjet_Added()
        {
            var list = new List<string>();
            var nonNullString = "string";
            list.AddIfNotNull(nonNullString);
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void AddIfNotNullOrWhiteSpace_NullObjet_NotAdded()
        {
            var list = new List<string>();
            string nullString = null;
            list.AddIfNotNullOrWhiteSpace(nullString);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void AddIfNotNullOrWhiteSpace_WhitepsaceObjet_NotAdded()
        {
            var list = new List<string>();
            var spacedString = "    ";
            list.AddIfNotNullOrWhiteSpace(spacedString);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void AddIfNotNullOrWhiteSpace_NotNullObjet_NotAdded()
        {
            var list = new List<string>();
            var stringString = "string";
            list.AddIfNotNull(stringString);
            Assert.Equal(1, list.Count);
        }
    }
}