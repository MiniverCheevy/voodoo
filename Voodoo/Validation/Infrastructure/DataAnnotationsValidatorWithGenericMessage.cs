using Voodoo.Infrastructure;

namespace Voodoo.Validation.Infrastructure
{
    public class DataAnnotationsValidatorWithGenericMessage : IValidator
    {
        public void Validate(object request)
        {
            if (request == null)
                throw new LogicException(Messages.RequestIsRequired);

            var validator = new DataAnnotationsValidator(request);
            if (validator.IsValid) return;
            const string message = Messages.ValidationErrorsOccured;
            var exception = new LogicException(message) {Details = validator.ValidationResultsAsNameValuePair};
            throw exception;
        }
    }
}