using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Infrastructure;

namespace Voodoo.Validation.Infrastructure
{
    public class DataAnnotationsValidatorWithGenericMessage : IValidator
    {
        public bool IsValid { get; protected set; }
        public void Validate(object request)
        {
            IsValid = false;

            if (request == null)
                throw new LogicException(Strings.Validation.requestIsRequired);

            var validator = new DataAnnotationsValidator(request);
            if (validator.IsValid)
            {
                IsValid = true;
                return;
            }
            string message = Strings.Validation.validationErrorsOccurred;
            var exception = new LogicException(message) {Details = validator.ValidationResultsAsNameValuePair};
            throw exception;
        }
    }
}