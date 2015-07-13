using Voodoo.Validation.Infrastructure;

namespace Voodoo.PCL.Validation.Infrastructure
{
    public class EmptyValidator : IValidator
    {
        public void Validate(object request)
        {
            IsValid = true;
        }

        public bool IsValid { get; protected set; }
    }
}