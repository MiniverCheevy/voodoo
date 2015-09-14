
#if (!PCL)

namespace Voodoo.Validation
{
    public class RequiredNonZeroInt : SafeValidationAttribute
    {
        public override bool IsValueValid(object value)
        {
            if (value == null)
                return false;

            var number = (int) value;

            if (number == 0)
                return false;

            return true;
        }
    }
}

#endif