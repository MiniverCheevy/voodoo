using Voodoo.Messages;
using Voodoo.Operations;

namespace Voodoo.Tests.TestClasses
{
    public class CommandThatDoesNotThrowErrors : Command<EmptyRequest, Response>
    {
        public CommandThatDoesNotThrowErrors(EmptyRequest request) : base(request)
        {
        }

        protected override Response ProcessRequest()
        {
            return response;
        }
    }
}