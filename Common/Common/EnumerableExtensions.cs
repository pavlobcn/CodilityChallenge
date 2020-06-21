using System;
using System.Collections.Generic;

namespace Common
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source,
            Action<TSource> action)
        {
            return ForEach(source, (item, index) => action(item));
        }

        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source,
            Action<TSource, int> action)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                action(item, index++);
            }

            return source;
        }
    }
}
