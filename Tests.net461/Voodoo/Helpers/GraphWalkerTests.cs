using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Voodoo;
using Voodoo.Helpers;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo.Helpers
{
    
    public class GraphWalkerTests
    {
        [Fact]
        public void GetDistinctTypes_SameTypeTwice_TypeReturnsOnce()
        {
            var walker = new GraphWalker(
                new GraphWalkerSettings {IncludeScalarTypes = false, TreatNullableTypesAsDistict = false},
                new Type[] {typeof(User), typeof(User)});
            var result = walker.GetDistinctTypes();
            result.Should().Contain(typeof(User));
            result.Should().Contain(typeof(Role));
        }

        [Fact]
        public void GetDistinctTypes_NoScalar_ShouldNotContainDateTime()
        {
            var walker = new GraphWalker(
                new GraphWalkerSettings {IncludeScalarTypes = false, TreatNullableTypesAsDistict = false},
                new Type[] {typeof(User), typeof(User)});
            var result = walker.GetDistinctTypes();
            result.Should().NotContain(typeof(DateTime));
        }

        [Fact]
        public void GetDistinctTypes_ScalarNoNullable_ShouldNotContainNullableDateTime()
        {
            var walker = new GraphWalker(
                new GraphWalkerSettings {IncludeScalarTypes = true, TreatNullableTypesAsDistict = false},
                new Type[] {typeof(User), typeof(User)});
            var result = walker.GetDistinctTypes();
            result.Should().Contain(typeof(DateTime));
            result.Should().NotContain(typeof(DateTime?));
        }

        [Fact]
        public void GetDistinctTypes_ScalarNullable_ShouldContainNullableDateTime()
        {
            var walker = new GraphWalker(
                new GraphWalkerSettings {IncludeScalarTypes = true, TreatNullableTypesAsDistict = true},
                new Type[] {typeof(User), typeof(User)});
            var result = walker.GetDistinctTypes();
            result.Should().Contain(typeof(DateTime));
            result.Should().Contain(typeof(DateTime?));
        }
    }
}