using System.ComponentModel.DataAnnotations;

namespace Voodoo.Tests.TestClasses
{
    public class RequestWithRequiredString
    {
        [Required]
        public string RequiredString { get; set; }
    }
}