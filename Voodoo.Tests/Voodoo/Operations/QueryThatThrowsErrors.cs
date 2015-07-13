using System;
using Voodoo.Infrastructure;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;
using Xunit;

namespace Voodoo.Tests.Voodoo.Operations
{
    public class QueryThatThrowsErrors : Query<EmptyRequest, TestingResponse>
    {
        public QueryThatThrowsErrors(EmptyRequest request) : base(request)
        {
        }

        protected override TestingResponse ProcessRequest()
        {
            throw new LogicException("Yowza", new Exception(TestingResponse.OhNo));
        }

        protected override void CustomErrorBehavior(Exception ex)
        {
            base.CustomErrorBehavior(ex);
            response.TestingData = TestingResponse.CustomErrorBehavior;
            Assert.NotNull(response.Exception);
            LogManager.Log(response.Exception.Message);
        }
    }
}