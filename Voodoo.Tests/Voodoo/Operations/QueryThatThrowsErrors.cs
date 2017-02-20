using System;
using Voodoo.Infrastructure;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo.Operations
{
    [TestClass]
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
        }
    }
}