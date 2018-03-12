using Voodoo.PCL.Validation.Infrastructure;

namespace Voodoo.Validation.Infrastructure
{
    public class ValidationManager
    {
        private static IValidator validator;

        public static IValidator Validator
        {
            get { return validator ?? (validator = GetDefaultValidatitor()); }
            set { validator = value; }
        }

        public static IValidator GetDefaultValidatitor()
        {
            return new DataAnnotationsValidatorWithGenericMessage();
        }

        public static void Validate(object @object)
        {
            Validator.Validate(@object);
        }
    }
}