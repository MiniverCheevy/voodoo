using Voodoo.Validation.Infrastructure;

namespace Voodoo
{
    public static class ValidationExtensions
    {
        public static bool Validate(this object request)
        {
            if (request == null)
                return true;
            try
            {
                var validator = ValidationManager.GetDefaultValidatitor();
                validator.Validate(request);
                return validator.IsValid;
            }
            catch
            {
                return false;
            }
        }
    }
}