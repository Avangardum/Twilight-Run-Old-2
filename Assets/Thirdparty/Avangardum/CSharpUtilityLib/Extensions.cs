using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Avangardum.CSharpUtilityLib
{
    public static class Extensions
    {
        private static Random _random = new Random();

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            var index = _random.Next(0, array.Length);
            return array[index];
        }
    }
}