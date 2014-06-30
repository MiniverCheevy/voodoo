using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Validation
{
    public class RequiredInt : SafeValidationAttribute
    {
        public override bool IsValueValid(object value)
        {
            if (value == null)
                return false;

            var number = (int) value;

            return true;
        }
    }
}