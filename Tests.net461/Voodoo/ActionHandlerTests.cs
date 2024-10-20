using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Voodoo.Messages;


namespace Voodoo.Tests.Voodoo
{
    
    public class ActionHandlerTests
    {
        private const string error = "Oh noes!!";
        private const string works = "Works on my box!";

        [Fact]
        public void Try_Exception_DoesNotThrow()
        {
            var response = ActionHandler.Try(() => { throw new Exception(error); });
            Assert.False(response.IsOk);
            Assert.Equal(error, response.Message);
        }

        [Fact]
        public void Try_NoException_IsOk()
        {
            var response = ActionHandler.Try(() => { });
            Assert.True(response.IsOk);
        }

        [Fact]
        public void Execute_Exception_DoesNotThrow()
        {
            var response = ActionHandler.Execute<Response>(() =>
            {
                var exception = new Exception(error);
                exception.Data.Add("key", "value");
                throw exception;
            });
            Assert.Equal(error, response.Message);
            Assert.False(response.IsOk);
        }

        [Fact]
        public void Execute_NoException_IsOk()
        {
            var response = ActionHandler.Execute(() => new Response() {Message = works});
            Assert.Equal(works, response.Message);
            Assert.True(response.IsOk);
        }


        [Fact]
        public async Task ExecuteAsync_Exception_DoesNotThrow()
        {
            var response = await ActionHandler.ExecuteAsync<Response>(() => { throw new Exception(error); });
            Assert.Equal(error, response.Message);
            Assert.False(response.IsOk);
        }

        [Fact]
        public async Task ExecuteAsync_NoException_IsOk()
        {
            var response = await ActionHandler.ExecuteAsync<Response>(async () =>
            {
                var result = await Task.Run(() => new Response {Message = works});
                return result;
            });
            Assert.Equal(works, response.Message);
            Assert.True(response.IsOk);
        }
    }
}