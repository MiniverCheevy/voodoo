using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo
{
    public static class ValidationExtensions
    {
        public static bool Validate(this object request)
        {
            if (request == null)
                return true;

            var validator = new DataAnnotationsValidator(request);
            return validator.IsValid;
        }
    }
}