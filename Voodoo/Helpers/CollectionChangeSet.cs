using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Helpers
{
    public class CollectionChangeSet
    {
        private readonly int[] left;
        private readonly int[] right;

        public CollectionChangeSet(IEnumerable<int> existing, IEnumerable<int> modified)
        {
            left = (existing ?? new int[] {}).ToArray();
            right = (modified ?? new int[] {}).ToArray();

            Added = right.Where(c => !left.Contains(c)).ToArray();
            Edited = right.Intersect(left).ToArray();
            Deleted = left.Where(c => !right.Contains(c)).ToArray();
        }

        public int[] Added { get; protected set; }
        public int[] Deleted { get; protected set; }
        public int[] Edited { get; protected set; }

        public bool AreDifferent()
        {
            if (Added.Any() || Deleted.Any())
                return true;

            return left.Count() != right.Count();
        }
    }
}