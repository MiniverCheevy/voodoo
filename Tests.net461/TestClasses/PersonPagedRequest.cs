using Voodoo.Messages;

namespace Voodoo.Tests.TestClasses
{
    public class PersonPagedRequest : PagedRequest

    {
        public string Text { get; set; }

        public override string DefaultSortMember
        {
            get { return "Id"; }
        }
    }
}