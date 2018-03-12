namespace Voodoo.Validation
{
    public class RequiredNonZeroInt : SafeValidationAttribute
    {
        protected override bool IsValueValid(object value)
        {
            if (value == null)
                return false;

            var number = (int) value;

            return number != 0;
        }
    }
}