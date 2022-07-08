using System;
using System.Collections.Generic;

namespace Query
{
    public static class Enumerable
    {
        //extension Method
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> logic)
        {
            List<TSource> resultAggregator = new List<TSource>();
            foreach (TSource item in source)
            {
                if (logic.Invoke(item))
                {
                    resultAggregator.Add(item);
                }
            }
            return resultAggregator;
        }
    }
}
