using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Voodoo.Messages;

namespace Voodoo
{
    public static class CollectionExtensions
    {
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

        public static ListResponse<T> ToListResponse<T>(this IEnumerable<T> items) where T : class, new()
        {
            var result = new ListResponse<T>();
            result.Data.AddRange(items);
            return result;
        }

        public static T[] ToArray<T>(this IEnumerable source)
        {
            var response = new List<T>();
            foreach (var item in source)
            {
                response.Add(item.To<T>());
            }
            return response.ToArray();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (var item in source.ToArray())
                action(item);

            return source;
        }

        /// <summary>
        ///     Indicates whether <paramref name="collection" /> contains any of the members in <paramref name="toFind" />.
        /// </summary>
        
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
    }
}