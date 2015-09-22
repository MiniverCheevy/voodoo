using System.Linq;
using Voodoo.Infrastructure;

#if (!PCL)

namespace Voodoo.Validation.Infrastructure
{
    public class DataAnnotationsValidatorWithFirstErrorAsMessage : IValidator

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
            var firstMessage = validator.ValidationResultsAsNameValuePair.First();
            var exception = new LogicException(firstMessage.Value);
            exception.Details = validator.ValidationResultsAsNameValuePair;
            throw exception;
        }
    }
}

#endif