using System;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Operations;
using Xunit;
using Voodoo.Tests.Voodoo.Logging;

namespace Voodoo.Tests.TestClasses
{
    public class CommandThatThrowsErrors : Command<EmptyRequest, TestingResponse>
    {
        public CommandThatThrowsErrors(EmptyRequest request) : base(request)
        {
        }

        protected override TestingResponse ProcessRequest()
        {
            throw new TestException(TestingResponse.OhNo);
        }

        protected override void CustomErrorBehavior(Exception ex)
        {
            base.CustomErrorBehavior(ex);
            response.TestingData = TestingResponse.CustomErrorBehavior;
        }
    }
}