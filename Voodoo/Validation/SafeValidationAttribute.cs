using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Voodoo.Validation
{
    public abstract class SafeValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                return IsValueValid(value);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public abstract bool IsValueValid(object value);
    }
}