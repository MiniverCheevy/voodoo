using System;
using Voodoo.Tests.TestClasses;
using Xunit;

namespace Voodoo.Tests.Voodoo
{
	public class IonicTests
	{
		[Fact]
		public void PathCombineLocal_PathStartsWithSlash_ReturnsCombinedPath()
		{
			var fragment = "\\abc\\efg";
			var path = IoNic.PathCombineLocal(@"C:\", fragment);
			Assert.True(path.StartsWith("C"));
		}

	}
}