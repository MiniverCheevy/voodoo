using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Validation
{
    public static class ValidationExtensions
    {
        //[DebuggerNonUserCode]
        public static bool Validate(this object request)
        {
            if (request == null)
                return true;

            var validator = new DataAnnotationsValidator(request);
            return validator.IsValid;
        }
    }
}