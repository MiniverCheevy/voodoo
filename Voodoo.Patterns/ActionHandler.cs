using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo.Helpers;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Operations;

namespace Voodoo
{
    public static class ActionHandler
    {

		public static Response Try<T>(Action action) 
		{
			var response = new Response();
			try
			{
				 action();
			}
			catch (Exception ex)
			{
				
				response.SetExceptions(ex);
				LogManager.Log(ex);
				if (VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging)
					response.Exception = null;
				return response;
			}
			return response;
		}

		public static T Execute<T>(Func<T> action) where T : IResponse, new()
        {
            var response = new T();
            try
            {
                return action();
            }
            catch (Exception ex)
            {

                response = new T { IsOk = false };                
                response.SetExceptions(ex);
                LogManager.Log(ex);
                if (VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging)
                    response.Exception = null;
                return response;
            }
        }
#if ! NET40
		public static async Task<T> ExecuteAsync<T>(Func<Task<T>> action) where T : IResponse, new()
        {
            var response = new T();
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                response = new T { IsOk = false };
                response.SetExceptions(ex);
                LogManager.Log(ex);
                if (VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging)
                    response.Exception = null;
                return response;
            }
        }
#endif
	}
}
