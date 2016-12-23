using System.ComponentModel.DataAnnotations;

namespace adlordy.Assignment.Models
{
    /// <summary>
    /// An JSON encoded base64 binary data.
    /// </summary>
    public class DiffModel
    {
        /// <summary>
        /// Binary array.
        /// </summary>
        /// <remarks>
        /// Required property.
        /// </remarks>
        [Required]
        public byte[] Data { get; set; }
    }
}
