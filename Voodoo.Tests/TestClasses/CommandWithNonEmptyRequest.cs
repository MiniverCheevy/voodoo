using Voodoo.Operations;

namespace Voodoo.Tests.TestClasses
{
    public class CommandWithNonEmptyRequest : Command<RequestWithRequiredString, TestingResponse>
    {
        public CommandWithNonEmptyRequest(RequestWithRequiredString request) : base(request)
        {
        }

        protected override TestingResponse ProcessRequest()
        {
            return response;
        }
    }
}