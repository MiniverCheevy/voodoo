using System;

namespace Voodoo.Validation
{
    public class RequiredDateTimeAttribute : SafeValidationAttribute
    {
        protected override bool IsValueValid(object value)
        {
            if (value == null)
                return false;

            var dateTime = (DateTime) value;

            return dateTime != DateTime.MinValue &&
                   dateTime != DateTime.MaxValue;
        }
    }
}