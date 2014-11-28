using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Infrastructure;

namespace Voodoo.Validation.Infrastructure
{
    public class DataAnnotationsValidatorWithGenericMessage : IValidator
    {
        public void Validate(object request)
        {
            if (request == null)
                throw new LogicException(Strings.Validation.requestIsRequired);

            var validator = new DataAnnotationsValidator(request);
            if (validator.IsValid) return;
            string message = Strings.Validation.validationErrorsOccurred;
            var exception = new LogicException(message) {Details = validator.ValidationResultsAsNameValuePair};
            throw exception;
        }
    }
}