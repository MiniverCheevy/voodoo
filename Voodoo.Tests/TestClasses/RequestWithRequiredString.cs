using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Voodoo.Tests.TestClasses
{
    public class RequestWithRequiredString
    {
        [Required]
        public string RequiredString { get; set; }
    }
}