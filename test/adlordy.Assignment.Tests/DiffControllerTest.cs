using adlordy.Assignment.Controllers;
using adlordy.Assignment.Models;
using adlordy.Assignment.Tests.Stub;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace adlordy.Assignment.Tests
{
    public class DiffControllerTest
    {
        DiffController _controller;

        public DiffControllerTest()
        {
            _controller = new DiffController(new StubStateService(), new StubDiffService());
        }

        [Fact]
        public void TestValidateEmptyLeft()
        {
            var actual = _controller.GetResult("1");
            Assert.IsAssignableFrom<NotFoundResult>(actual);
        }

        [Fact]
        public void TestValidateEmptyRight()
        {
            _controller.Set("1", Side.Left, new DiffModel { Data = new byte[] { 0, 1, 2 } });
            var actual = _controller.GetResult("1");
            Assert.IsAssignableFrom<NotFoundResult>(actual);
        }

        [Fact]
        public void TestOk()
        {
            _controller.Set("1", Side.Left, new DiffModel { Data = new byte[] { 0, 1, 2 } });
            _controller.Set("1", Side.Right, new DiffModel { Data = new byte[] { 0, 1, 2 } });
            var actual = _controller.GetResult("1");
            Assert.IsAssignableFrom<OkObjectResult>(actual);
        }
    }
}
