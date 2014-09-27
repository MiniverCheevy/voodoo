using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Validation
{
    public class RequiredGuid : SafeValidationAttribute
    {
        public override bool IsValueValid(object value)
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