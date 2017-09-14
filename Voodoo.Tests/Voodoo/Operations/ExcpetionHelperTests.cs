using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo.Operations
{
    [TestClass]
    public class ExcpetionHelperTests
    {
        [TestMethod]
        public void Exception_NonStandardPropsAreRead()
        {
            var ex = new ReflectionTypeLoadException(new Type[] { typeof(string) }, new Exception[] { new Exception() });
            ExceptionHelper.HandleException(ex, typeof(QueryThatDoesNotThrowErrors), new IdRequest());
            ex.Data.Keys.Should().Contain("ReflectionTypeLoadException.LoaderExceptions");
        }
        [TestMethod]
        public void InnerException_NonStandardPropsAreRead()
        {
            var ex = new ReflectionTypeLoadException(new Type[] { typeof(string) }, new Exception[] { new Exception() });
            var outerEx = new Exception("Yikes",ex);
            ExceptionHelper.HandleException(outerEx, typeof(QueryThatDoesNotThrowErrors), new IdRequest());
            outerEx.Data.Keys.Should().Contain("ReflectionTypeLoadException.LoaderExceptions");
        }
    }
}
