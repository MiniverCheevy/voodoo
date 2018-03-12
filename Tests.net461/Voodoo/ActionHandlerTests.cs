using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;


namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class ActionHandlerTests
    {
        private const string error = "Oh noes!!";
        private const string works = "Works on my box!";

        [TestMethod]
        public void Try_Exception_DoesNotThrow()
        {
            var response = ActionHandler.Try(() => { throw new Exception(error); });
            Assert.AreEqual(false, response.IsOk);
            Assert.AreEqual(error, response.Message);
        }

        [TestMethod]
        public void Try_NoException_IsOk()
        {
            var response = ActionHandler.Try(() => { });
            Assert.AreEqual(true, response.IsOk);
        }

        [TestMethod]
        public void Execute_Exception_DoesNotThrow()
        {
            var response = ActionHandler.Execute<Response>(() =>
            {
                var exception = new Exception(error);
                exception.Data.Add("key", "value");
                throw exception;
            });
            Assert.AreEqual(error, response.Message);
            Assert.AreEqual(false, response.IsOk);
        }

        [TestMethod]
        public void Execute_NoException_IsOk()
        {
            var response = ActionHandler.Execute(() => new Response() {Message = works});
            Assert.AreEqual(works, response.Message);
            Assert.AreEqual(true, response.IsOk);
        }


        [TestMethod]
        public async Task ExecuteAsync_Exception_DoesNotThrow()
        {
            var response = await ActionHandler.ExecuteAsync<Response>(async () => { throw new Exception(error); });
            Assert.AreEqual(error, response.Message);
            Assert.AreEqual(false, response.IsOk);
        }

        [TestMethod]
        public async Task ExecuteAsync_NoException_IsOk()
        {
            var response = await ActionHandler.ExecuteAsync<Response>(async () =>
            {
                var result = await Task.Run(() => new Response {Message = works});
                return result;
            });
            Assert.AreEqual(works, response.Message);
            Assert.AreEqual(true, response.IsOk);
        }
    }
}