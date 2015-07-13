using System.ComponentModel.DataAnnotations;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithCompareValidator
    {
        [Required]
        public string Password { get; set; }

        [Required, Compare("Password"), Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}