using System.Linq;
using Voodoo.Infrastructure;

namespace Voodoo.Validation.Infrastructure
{
    public class DataAnnotationsValidatorWithFirstErrorAsMessage : IValidator

    {
        public void Validate(object request)
        {
            if (request == null)
                throw new LogicException(Messages.RequestIsRequired);

            var validator = new DataAnnotationsValidator(request);
            if (validator.IsValid) return;
            var firstMessage = validator.ValidationResultsAsNameValuePair.First();
            var exception = new LogicException(firstMessage.Value);
            exception.Details = validator.ValidationResultsAsNameValuePair;
            throw exception;
        }
    }
}