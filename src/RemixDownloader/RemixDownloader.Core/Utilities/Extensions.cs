using System;
using System.Collections.Generic;
using System.Linq;

namespace RemixDownloader.Core.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Copied from .NET Core to share across platforms
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> SkipLast<TSource>(
            this IEnumerable<TSource> source,
            int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (count <= 0)
                return source.Skip<TSource>(0);
            return SkipLastIterator<TSource>(source, count);
        }

        private static IEnumerable<TSource> SkipLastIterator<TSource>(
            IEnumerable<TSource> source,
            int count)
        {
            Queue<TSource> queue = new Queue<TSource>();
            using (IEnumerator<TSource> e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (queue.Count == count)
                    {
                        do
                        {
                            yield return queue.Dequeue();
                            queue.Enqueue(e.Current);
                        }
                        while (e.MoveNext());
                        break;
                    }
                    queue.Enqueue(e.Current);
                }
            }
        }
    }
}
