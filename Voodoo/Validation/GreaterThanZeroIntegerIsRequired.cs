
#if (!PCL)

namespace Voodoo.Validation
{
    public class GreaterThanZeroIntegerIsRequired : SafeValidationAttribute
    {
        public override bool IsValueValid(object value)
        {
            if (value == null)
                return false;
            var converted = value.To<int>();
            return converted > 0;
        }
    }
}

#endif