namespace adlordy.Assignment.Models
{
    /// <summary>
    /// A class describing an partical diff in between binary arrays.
    /// </summary>
    public class DiffResultItem
    {
        /// <summary>
        /// Offset of the diff.
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// Length of the diff.
        /// </summary>
        public int Length { get; set; }
    }
}