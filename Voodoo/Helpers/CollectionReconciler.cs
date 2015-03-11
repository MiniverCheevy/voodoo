using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Helpers
{
    public class CollectionReconciler<TExisting, TModified, TKey>
    {
        private readonly TKey[] left;
        private readonly TKey[] right;

        public CollectionReconciler(IEnumerable<TExisting> existing, IEnumerable<TModified> modified,
            Func<TExisting,TKey> existingKey, Func<TModified,TKey> modifiedKey)
        {
            existing = (existing ?? new TExisting[] { }).ToArray();
            modified = (modified ?? new TModified[] { }).ToArray();

            left = existing.Select(existingKey).ToArray();
            right = modified.Select(modifiedKey).ToArray();

            AddedKeys = right.Where(c => !left.Contains(c)).ToArray();
            EditedKeys = right.Intersect(left).ToArray();
            DeletedKeys = left.Where(c => !right.Contains(c)).ToArray();

            Added = modified.Where(c => AddedKeys.Contains(modifiedKey(c))).ToArray();
            Deleted = existing.Where(c => DeletedKeys.Contains(existingKey(c))).ToArray();
            Edited = existing.Join(modified, existingKey, modifiedKey,
                (e, m) => new EditedItem<TExisting, TModified, TKey> {Existing = e, Modified = m, Key = existingKey(e)})
                .ToArray();
        }

        public EditedItem<TExisting, TModified, TKey>[] Edited { get; set; }

        public TExisting[] Deleted { get; set; }

        public TModified[] Added { get; set; }

        public TKey[] AddedKeys { get; protected set; }
        public TKey[] DeletedKeys { get; protected set; }
        public TKey[] EditedKeys { get; protected set; }
       
    }

    public class EditedItem<TExisting, TModified, TKey>
    {
        public TExisting Existing { get; set; }
        public TModified Modified { get; set; }
        public TKey Key { get; set; }
    }
}