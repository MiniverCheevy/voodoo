using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Operations;

namespace Voodoo.Tests.TestClasses
{
    public class CommandThatThrowsErrors : Command<EmptyRequest, TestingResponse>
    {
        public CommandThatThrowsErrors(EmptyRequest request) : base(request)
        {
        }

        protected override TestingResponse ProcessRequest()
        {
            throw new Exception(TestingResponse.OhNo);
        }

        protected override void CustomErrorBehavior(Exception ex)
        {
            base.CustomErrorBehavior(ex);
            response.TestingData = TestingResponse.CustomErrorBehavior;
            Assert.NotNull(response.Exception);
            LogManager.Log(response.Exception);
        }
    }
}