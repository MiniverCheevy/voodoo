using System;

#if (!PCL)

namespace Voodoo.Validation
{
    public class RequiredDateTimeAttribute : SafeValidationAttribute
    {
        public override bool IsValueValid(object value)
        {
            if (value == null)
                return false;

            var dateTime = (DateTime) value;

            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
                return false;

            return true;
        }
    }
}

#endif