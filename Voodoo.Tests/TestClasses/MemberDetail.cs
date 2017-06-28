using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Infrastructure.Notations;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class MemberDetail
    {
        [UI(IsHidden = true)]
        public int Id { get; set; }

        [StringLength(128, ErrorMessage = "name too long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(128, ErrorMessage = "title too long")]
        public string Title { get; set; }

        [Display(Name = "Required Int")]
        [RequiredNonZeroInt(ErrorMessage = "required")]
        public int RequiredInt { get; set; }

        [Display(Name = "Optional Int")]
        public int? OptionalInt { get; set; }

        [Display(Name = "Required Date")]
        [Required(ErrorMessage = "required")]
        [Range(typeof(DateTime), "1/1/1900", "3/4/2050", ErrorMessage = "invalid date")]
        public DateTime RequiredDate { get; set; }

        [Display(Name = "Optional Date")]
        [Range(typeof(DateTime), "1/1/1900", "3/4/2050", ErrorMessage = "required")]
        public DateTime? OptionalDate { get; set; }

        [Display(Name = "Required Decimal")]
        [Required(ErrorMessage = "required")]
        public decimal RequiredDecimal { get; set; }

        [Display(Name = "Optional Decimal")]
        public decimal? OptionalDecimal { get; set; }

        [Display(Name = "Manager")]
        [RequiredNonZeroInt(ErrorMessage = "required")]
        public int? ManagerId { get; set; }
    }
}
