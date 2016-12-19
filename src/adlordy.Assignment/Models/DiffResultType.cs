namespace adlordy.Assignment.Models
{
    /// <summary>
    /// Diff result type.
    /// </summary>
    public enum DiffResultType
    {
        /// <summary>
        /// Binary data is equals
        /// </summary>
        Equals,
        /// <summary>
        /// Sizes of binary data are not equal.
        /// </summary>
        SizeDoNotMatch,
        /// <summary>
        /// Sizes of the binary data are equal but contents are not.
        /// </summary>
        ContentDoNotMatch
    }
}