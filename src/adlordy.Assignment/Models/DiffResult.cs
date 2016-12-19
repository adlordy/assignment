using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace adlordy.Assignment.Models
{
    /// <summary>
    /// A class describing the result of the diff between two binary arrays.
    /// </summary>
    public class DiffResult
    {
        /// <summary>
        /// Empty public contructor.
        /// </summary>
        public DiffResult()
        {
        }

        /// <summary>
        /// Helper contructor for detailed diff with ContentDoNotMatch.
        /// </summary>
        /// <param name="diffs">A detailed diff.</param>
        public DiffResult(IEnumerable<DiffResultItem> diffs) : this(DiffResultType.ContentDoNotMatch)
        {
            Diffs = diffs;
        }

        /// <summary>
        /// Helper constructor Equal and SizesDoesNotMatch types.
        /// </summary>
        /// <param name="resultType">Result Type</param>
        public DiffResult(DiffResultType resultType)
        {
            DiffResultType = resultType;
        }
        /// <summary>
        /// Diff result type.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DiffResultType DiffResultType { get; set; }

        /// <summary>
        /// A resulting diffs offsets and lengths.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<DiffResultItem> Diffs { get; set; }
    }
}
