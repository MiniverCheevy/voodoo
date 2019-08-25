namespace Voodoo.Infrastructure
{
    public class NotFoundException : LogicException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}