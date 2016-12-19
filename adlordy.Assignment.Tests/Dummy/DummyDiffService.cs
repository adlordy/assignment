using adlordy.Assignment.Contracts;
using adlordy.Assignment.Models;

namespace adlordy.Assignment.Tests.Dummy
{
    /// <summary>
    /// Dummy IDiffService implementation for unit testing.
    /// </summary>
    internal class DummyDiffService : IDiffService
    {
        /// <summary>
        /// Always returns Equals. 
        /// </summary>
        /// <param name="left">Left array.</param>
        /// <param name="right">Right array.</param>
        /// <returns>Equals return type</returns>
        public DiffResult GetDiff(byte[] left, byte[] right)
        {
            return new DiffResult(DiffResultType.Equals);
        }
    }
}
