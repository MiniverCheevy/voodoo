using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
