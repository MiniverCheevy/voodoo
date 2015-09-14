
#if (PCL)
using Voodoo.PCL.Validation.Infrastructure;
#endif

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
#if PCL
            return new EmptyValidator();
#else
            return new DataAnnotationsValidatorWithGenericMessage();
#endif
        }

        public static void Validate(object @object)
        {
            Validator.Validate(@object);
        }
    }
}