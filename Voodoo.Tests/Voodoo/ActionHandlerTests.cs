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
			Assert.Equal(false, response.IsOk);
			Assert.Equal(error, response.Message);

		}

		[Fact]
		public void Try_NoException_IsOk()
		{
			var response = ActionHandler.Try(() => {  });
			Assert.Equal(true, response.IsOk);

		}

		[Fact]
		public void Execute_Exception_DoesNotThrow()
		{
			var response = ActionHandler.Execute(() => {
				throw new Exception(error);
				return new Response();
			});
			Assert.Equal(error, response.Message);
			Assert.Equal(false, response.IsOk);

		}

        //[Fact]
        //public void Execute_ NoException_IsOk()
        //{
        //	var response = ActionHandler.Execute(() => {
        //		return new Response() {Message=works};
        //	});
        //	Assert.Equal(works, response.Message);
        //	Assert.Equal(true, response.IsOk);

        //}
#if !NET40 && !DNX
  //      [Fact]
		//public async Task ExecuteAsync_Exception_DoesNotThrow()
		//{
		//	var response = await ActionHandler.ExecuteAsync<Response>(async () => {
		//		throw new Exception(error);
		//		await Task.Run(() => { return new Response(); });
		//	});
		//	Assert.Equal(error, response.Message);
		//	Assert.Equal(false, response.IsOk);

		//}
#endif
        //[Fact]
        //public async Task ExecuteAsync_NoException_IsOk()
        //{
        //	var response = await ActionHandler.ExecuteAsync<Response>(async () => {
        //		await Task.Run(() => { return new Response() {Message=works}; });
        //	});
        //	Assert.Equal(error, response.Message);
        //	Assert.Equal(false, response.IsOk);

        //}
    }
}
