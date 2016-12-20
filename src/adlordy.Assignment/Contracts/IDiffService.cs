using adlordy.Assignment.Models;

namespace adlordy.Assignment.Contracts
{
    /// <summary>
    /// Contract for Diff operation.
    /// </summary>
    public interface IDiffService
    {
        /// <summary>
        /// Calculates the diff between two binary array and returns <see cref="DiffResult"/> 
        /// that contains difff type <see cref="DiffResultType"/> and diff details in the <see cref="DiffResultItem"/>.
        /// </summary>
        /// <param name="left">Left binary argument</param>
        /// <param name="right">Right binary argument</param>
        /// <returns>Result of the diff in <see cref="DiffResult"/> type.</returns>
        DiffResult GetDiff(byte[] left, byte[] right);
    }
}
