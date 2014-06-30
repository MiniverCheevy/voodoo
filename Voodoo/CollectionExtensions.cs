using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Voodoo
{
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Indicates whether <paramref name="collection" /> contains any of the members in <paramref name="toFind" />.
        /// </summary>
        [DebuggerNonUserCode]
        public static bool ContainsAny<T>(this ICollection<T> collection, ICollection<T> toFind)
        {
            var found = false;

            foreach (var item in toFind)
            {
                if (!collection.Contains(item))
                    continue;
                found = true;
                break;
            }

            return found;
        }

        /// <summary>
        ///     Determines whether <paramref name="collection" /> contains all of the members of the <paramref name="toFind" />
        ///     collection.
        /// </summary>
        [DebuggerNonUserCode]
        public static bool ContainsAll<T>(this ICollection<T> collection, ICollection<T> toFind)
        {
            bool foundAll;

            if (toFind.Count == 0)
            {
                foundAll = false;
            }
            else
            {
                foundAll = true;

                foreach (var item in toFind)
                {
                    if (collection.Contains(item))
                        continue;
                    foundAll = false;
                    break;
                }
            }
            return foundAll;
        }

        /// <summary>
        ///     Creates an array of the items in <paramref name="collection" />.
        /// </summary>
        [DebuggerNonUserCode]
        public static T[] ToArray<T>(this ICollection collection)
        {
            var array = new T[collection.Count];
            var index = 0;

            foreach (T item in collection)
            {
                array[index++] = item;
            }

            return array;
        }

        /// <summary>
        ///     Creates an array of the items in <paramref name="collection" />.
        /// </summary>
        [DebuggerNonUserCode]
        public static T[] ToArray<T>(this ICollection<T> collection)
        {
            var array = new T[collection.Count];
            var index = 0;

            foreach (var item in collection)
            {
                array[index++] = item;
            }

            return array;
        }

        public static void AddIfNotNull<T>(this ICollection<T> collection, T item) where T : class
        {
            if (item != null)
                collection.Add(item);
        }

        public static void AddIfNotNullOrWhiteSpace(this ICollection<string> collection, object item)
        {
            if (item != null && !string.IsNullOrWhiteSpace(item.ToString()))
                collection.Add(item.ToString());
        }
    }
}