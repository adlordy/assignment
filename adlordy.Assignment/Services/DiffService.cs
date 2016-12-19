using adlordy.Assignment.Contracts;
using System;
using adlordy.Assignment.Models;
using System.Collections.Generic;
using System.Linq;

namespace adlordy.Assignment.Services
{
    /// <summary>
    /// Implementation of diff service. <see cref="IDiffService"/> for details.
    /// </summary>
    public class DiffService : IDiffService
    {
        /// <summary>
        /// Implementation of the diff operation.
        /// </summary>
        /// <param name="left">Left argument</param>
        /// <param name="right">Right argument</param>
        /// <returns>
        ///     <see cref="DiffResult"/>
        /// </returns>
        public DiffResult GetDiff(byte[] left, byte[] right)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");

            if (left.Length != right.Length)
            {
                return new DiffResult(DiffResultType.SizeDoNotMatch);
            }

            var diff = GetDiffItems(left,right).ToList();
            if (diff.Count == 0)
                return new DiffResult(DiffResultType.Equals);

            return new DiffResult(diff);
        }

        /// <summary>
        /// The actual diff routine.
        /// </summary>
        /// <param name="left">Left argument</param>
        /// <param name="right">Right argument</param>
        /// <returns>An IEnumerable of <see cref="DiffResultItem"/>. Yields an item for each actual diff.</returns>
        /// <remarks>
        /// A yield factory for chosen for a readability and ease of implementation. 
        /// In case only first diff is needed in the future this implementation 
        /// will allow to be called as FirstOfDefault() and will exist on when first diff is found.
        /// </remarks>
        private static IEnumerable<DiffResultItem> GetDiffItems(byte[] left, byte[] right)
        {
            var diffLength = 0;
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] == right[i])
                {
                    if (diffLength > 0)
                    {
                        yield return GetResult(diffLength, i);
                        diffLength = 0;
                    }
                }
                else
                {
                    diffLength++;
                }
            }
            if (diffLength > 0)
            {
                yield return GetResult(diffLength, left.Length);
            }
        }
        /// <summary>
        /// Helper method to produce result item.
        /// </summary>
        /// <param name="diffLength">The length of the diff.</param>
        /// <param name="index">Current counter value</param>
        /// <returns>Result item.</returns>
        private static DiffResultItem GetResult(int diffLength, int index)
        {
            return new DiffResultItem
            {
                Length = diffLength,
                Offset = index - diffLength
            };
        }
    }
}
