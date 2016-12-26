using adlordy.Assignment.DockerTest.Models;
using adlordy.Assignment.DockerTest.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace adlordy.Assignment.DockerTest.Controllers
{
    [Route("v1/[controller]")]
    public class RunTestController : Controller
    {
        private readonly RunTestService _service;

        public RunTestController(RunTestService service)
        {
            _service = service;
        }
        [HttpPost("")]
        public async Task<IActionResult> Run([FromBody] RunTestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var results = await _service.RunTest(model.Url);
                return Ok(results);
            } catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = ex.Message });
            }
        }
    }
}
