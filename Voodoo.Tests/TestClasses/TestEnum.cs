#if !PCL
using System.ComponentModel.DataAnnotations;
#endif
using System.ComponentModel;

namespace Voodoo.Tests.TestClasses
{
    public enum TestEnum
    {
        Red = 1,
        Blue = 2,
        RedOrangeYellow = 3
    }

#if !DNXCORE50 && !PCL
	public enum TestEnumWithDescriptionAndDisplay
	{
		[Description("Crimson")]
		Red = 1,
		[Display(Name="Azure")]
		Blue = 2,
		RedOrangeYellow = 3
	}
#endif
}