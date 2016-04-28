using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Voodoo.Tests
{
	public class Scratch
	{
		[Fact]
		public void test()
		{
			IoNic.WriteFile("blah",
				@"Z:\Dropbox\Lib\Projects\spawn-templates\SPAv2\Fernweh.Core\Operations\Users\Extras\UserExtensions.generated.cs");
		}
	}
}
