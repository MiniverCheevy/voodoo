using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo.Operations.Async
{
    [TestClass]
    public class ExecutorTests
    {
        [TestMethod]
        public async Task Execute_ThrowsException_IsNotOk()
        {
            
            var response = await new ExecutorAsyncThatThrowsExceptions(new EmptyRequest()).ExecuteAsync();
            Assert.AreEqual("Boom", response.Message);
            Assert.AreEqual(false, response.IsOk);

        }
    }
}
