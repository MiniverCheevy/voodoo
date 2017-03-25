namespace Voodoo.Messages
{
    public class NameIdPair : INameIdPair
    {
        public NameIdPair()
        {
        }

        public NameIdPair(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"NameId Name:{Name.To<string>()} Id:{Id.To<string>()}";
        }
    }
}