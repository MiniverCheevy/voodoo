using System;
using System.Collections.Generic;

namespace Voodoo.Messages
{
    public class Grouping<T>
    {
        public Grouping()
        {
            Id = Guid.NewGuid();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public List<T> Data { get; set; }
    }
}