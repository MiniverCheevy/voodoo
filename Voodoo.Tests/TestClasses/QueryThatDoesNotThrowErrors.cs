using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Operations;

namespace Voodoo.Tests.TestClasses
{
    public class QueryThatDoesNotThrowErrors : Query<EmptyRequest, Response>
    {
        public QueryThatDoesNotThrowErrors(EmptyRequest request) : base(request)
        {
        }

        protected override Response ProcessRequest()
        {
            return response;
        }
    }
}