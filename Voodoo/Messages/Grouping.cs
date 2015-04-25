using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voodoo.Messages
{
    public class Grouping<T>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public List<T> Data { get; set; }
        public Grouping()
        {
            Id = Guid.NewGuid();
        }

    }
}
