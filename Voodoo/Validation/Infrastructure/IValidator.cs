namespace Voodoo.Validation.Infrastructure
{
    public interface IValidator
    {
        bool IsValid { get; }
        void Validate(object request);
    }
}