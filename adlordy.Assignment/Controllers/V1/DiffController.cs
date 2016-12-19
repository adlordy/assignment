using adlordy.Assignment.Contracts;
using adlordy.Assignment.Models;
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
        /// Reference to the service that does the actual diff.
        /// </summary>
        private readonly IDiffService _diffService;
        private readonly IStateService _stateService;

        /// <summary>
        /// A constructor that accepts IDiffService contract implemetation.
        /// </summary>
        public DiffController(IStateService stateService, IDiffService diffService)
        {
            _stateService = stateService;
            _diffService = diffService;
        }

        /// <summary>
        /// Sets the left or right side of the diff arguments.
        /// </summary>
        /// <returns>Action result</returns>
        [HttpPut, Route("{id}/{side}")]
        public IHttpActionResult Set([FromUri] string id, [FromUri] Side side, [FromBody] DiffModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _stateService.Set(id, side, model.Data);
            return StatusCode(HttpStatusCode.Accepted);
        }

        /// <summary>
        /// Gets the result of diff for the arguments set by v1/diff/left and v1/diff/right calls.
        /// </summary>
        /// <returns>Action result. <see cref="DiffResult"/> </returns>
        [HttpGet, Route("{id}")]
        public IHttpActionResult GetResult([FromUri] string id)
        {
            var left = _stateService.Get(id, Side.Left);
            if (left == null)
                return NotFound();

            var right = _stateService.Get(id, Side.Right);
            if (right == null)
                return NotFound();

            return Ok(_diffService.GetDiff(left, right));
        }
    }
}
