using System;
using System.Collections.Generic;

namespace IslandOfMisfitTypes.Linq
{
    /// <summary>
    /// Adds 'Batch' extensions to <see cref="IEnumerable{T}"/> to split the collection into a
    /// a series of enumerables each starting where the last one ended.
    /// </summary>
    public static class BatchExtension
    {
        /// <summary>
        /// Enumerates <paramref name="target"/> as a series of <see cref="IEnumerable{T}"/>
        /// intsances whose lengths are specified by <paramref name="batchSizes"/>.
        /// </summary>
        /// <typeparam name="T">The type of the element.</typeparam>
        /// <param name="target">The <see cref="IEnumerable{T}"/> to batch.</param>
        /// <param name="batchSizes">The sizes of each subsequent batch.</param>
        /// <returns>The batches.</returns>
        /// <remarks>
        /// If the sum of <paramref name="batchSizes"/> is less than the Count() of
        /// <paramref name="target"/> then the remaining elements will be skipped. If a batch size
        /// is greater than or equal to the number of remaining elements the the next batch will
        /// will be the final batch and include all remaining elements. Subsequent values in
        /// <paramref name="batchSizes"/> will be ignored.
        /// </remarks>
        public static IEnumerable<IEnumerable<T>> Batch<T>(
            this IEnumerable<T> target, IEnumerable<int> batchSizes)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (batchSizes == null) throw new ArgumentNullException(nameof(batchSizes));

            // Move the actual IEnuerable implementation to another method so the parameter
            // wont be deferred until evalutation.
            return BatchImpl(target, batchSizes);
        }

        private static IEnumerable<IEnumerable<T>> BatchImpl<T>(
            this IEnumerable<T> target, IEnumerable<int> batchSizes)
        {
            var source = target?.GetEnumerator();
            foreach (var batchSize in batchSizes)
            {
                if (batchSize <= 0) yield return new T[0];
                else if (source.MoveNext()) yield return GetBatch(source, batchSize);
            }
        }

        private static IEnumerable<T> GetBatch<T>(IEnumerator<T> source, int count)
        {
            if (count <= 0) return new T[0];
            var batch = new List<T>();
            do { batch.Add(source.Current); } while (--count > 0 && source.MoveNext());
            return batch;
        }
    }
}
