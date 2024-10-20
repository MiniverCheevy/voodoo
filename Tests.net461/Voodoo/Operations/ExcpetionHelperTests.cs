using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo.Operations
{
    
    public class ExcpetionHelperTests
    {
        private const string exceptionType = "ReflectionTypeLoadException.LoaderExceptions";

        [Fact]
        public void Exception_NonStandardPropsAreRead()
        {
            var ex = new ReflectionTypeLoadException(new Type[] {typeof(string)}, new Exception[] {new Exception()});
            ExceptionHelper.HandleException(ex, typeof(QueryThatDoesNotThrowErrors), new IdRequest());
            Assert.Contains(ex.Data.Keys.ToArray<string>(), c => c.Contains(exceptionType));
        }

        [Fact]
        public void InnerException_NonStandardPropsAreRead()
        {
            var ex = new ReflectionTypeLoadException(new Type[] {typeof(string)}, new Exception[] {new Exception()});
            var outerEx = new Exception("Yikes", ex);
            ExceptionHelper.HandleException(outerEx, typeof(QueryThatDoesNotThrowErrors), new IdRequest());            
            Assert.Contains(outerEx.Data.Keys.ToArray<string>(), c => c.Contains(exceptionType));
        }
    }
}