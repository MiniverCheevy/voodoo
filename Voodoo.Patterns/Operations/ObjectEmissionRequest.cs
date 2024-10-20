namespace Voodoo.Operations
{
    public class ObjectEmissionRequest
    {
        public object Source { get; set; }
        public string Name { get; set; }

        public bool IncludeNull { get; set; } 

        public bool IncludeDafaultValues { get; set; }
    }
}