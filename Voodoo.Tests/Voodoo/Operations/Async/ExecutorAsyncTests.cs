using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#if !NET40
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
            Assert.AreEqual(false, response.ExecuteFinished);
        }

        [TestMethod]
        public async Task Execute_FailsValidation_IsNotOk()
        {
            var response = await new ExecutorAsyncThatFailsValidation(new EmptyRequest()).ExecuteAsync();
            Assert.AreEqual("Boom", response.Message);
            Assert.AreEqual(false, response.IsOk);
            Assert.AreEqual(false, response.ExecuteFinished);
        }
    }
}
#endif