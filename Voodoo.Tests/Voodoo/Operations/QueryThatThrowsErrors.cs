using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo;
using System.Linq;
using System.Collections.Generic;
using System;
using Voodoo.Infrastructure;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;

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
            Assert.IsNotNull(response.Exception);
            LogManager.Log(response.Exception.Message);
        }
    }
}