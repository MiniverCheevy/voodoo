
#if (!PCL)

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

#endif