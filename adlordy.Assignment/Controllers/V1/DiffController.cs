using adlordy.Assignment.Contracts;
using adlordy.Assignment.Models;
using adlordy.Assignment.Services;
using System.Net;
using System.Web.Http;

namespace adlordy.Assignment.Controllers.V1
{
    /// <summary>
    /// Diff controller with base path of v1/diff.
    /// </summary>
    [RoutePrefix("v1/diff")]
    public class DiffController : ApiController
    {
        /// <summary>
        /// Private static variables to hold application state.
        /// </summary>
        /// <remarks>
        /// Since there are no requirements on persistance of the application state the simplest approach of storing in memory was chosen.
        /// This can be futher refactored to support storage in different place/
        /// </remarks>
        private static byte[] _left, _right;
        /// <summary>
        /// Reference to the service that does the actual diff.
        /// </summary>
        private readonly IDiffService _diffService;

        /// <summary>
        /// A constructor that accepts IDiffService contract implemetation.
        /// </summary>
        /// <param name="diffService">Diff Service</param>
        public DiffController(IDiffService diffService)
        {
            _diffService = diffService;
        }

        /// <summary>
        /// This is a default constructor for demo purposes only. In a real application the service implementatioon will be injected 
        /// by DI framework using method above.
        /// </summary>
        public DiffController() : this(new DiffService())
        {
        }

        /// <summary>
        /// Sets the left side of the diff arguments.
        /// </summary>
        /// <param name="left">Left argument</param>
        /// <returns>Action result</returns>
        [HttpPost, Route("left")]
        public IHttpActionResult SetLeft([FromBody] DiffModel left)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _left = left.Data;
            return Ok(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Sets the right side of the diff arguments.
        /// </summary>
        /// <param name="right">Right argument</param>
        /// <returns>Action result</returns>
        [HttpPost, Route("right")]
        public IHttpActionResult SetRight([FromBody] DiffModel right)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _right = right.Data;
            return Ok(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Gets the result of diff for the arguments set by v1/diff/left and v1/diff/right calls.
        /// </summary>
        /// <returns>Action result. <see cref="DiffResult"/> </returns>
        [HttpGet, Route("")]
        public IHttpActionResult GetResult()
        {
            if (_left == null)
                return BadRequest("Left not set");
            if (_right == null)
                return BadRequest("Right not set");
            return Ok(_diffService.GetDiff(_left, _right));
        }
    }
}
