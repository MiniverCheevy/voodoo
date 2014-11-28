using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Validation.Infrastructure
{
    public class ValidationManager
    {
        private static IValidator validator;

        public static IValidator Validator
        {
            get { return validator ?? (validator = new DataAnnotationsValidatorWithGenericMessage()); }
            set { validator = value; }
        }

        public static void Validate(object @object)
        {
            Validator.Validate(@object);
        }
    }
}