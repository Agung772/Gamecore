using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace ACore
{
    public static class RandomExtensions
    {
        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
                throw new InvalidOperationException("List is null or empty.");

            var _index = Random.Range(0, list.Count);
            return list[_index];
        }

        public static T GetRandom<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
                throw new InvalidOperationException("Array is null or empty.");

            var _index = Random.Range(0, array.Length);
            return array[_index];
        }
        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null || !enumerable.Any())
            {
                throw new InvalidOperationException("Collection is null or empty.");
            }

            var _index = Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(_index);
        }
    }
}