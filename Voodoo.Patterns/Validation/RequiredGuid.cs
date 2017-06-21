using System;

#if (!PCL)

namespace Voodoo.Validation
{
    public class RequiredGuid : SafeValidationAttribute
    {
        protected override bool IsValueValid(object value)
        {
            if (value == null)
                return false;

            var guid = Guid.Parse(value.ToString());

            if (guid == Guid.Empty)
                return false;

            return true;
        }
    }
}

#endif