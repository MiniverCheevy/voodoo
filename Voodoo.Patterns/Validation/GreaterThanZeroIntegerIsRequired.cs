
using System;

#if (!PCL)

namespace Voodoo.Validation
{
    [Obsolete("User RequiredNonZeroInt instead")]
    public class GreaterThanZeroIntegerIsRequired : SafeValidationAttribute
    {
        protected override bool IsValueValid(object value)
        {
            var converted = value?.To<int>();
            return converted > 0;
        }
    }
}

#endif