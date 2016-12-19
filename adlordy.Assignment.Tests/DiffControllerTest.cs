using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adlordy.Assignment.Controllers.V1;
using adlordy.Assignment.Tests.Stub;
using adlordy.Assignment.Models;
using System.Web.Http.Results;

namespace adlordy.Assignment.Tests
{
    [TestClass]
    public class DiffControllerTest
    {
        DiffController _controller;

        [TestInitialize]
        public void TestInit()
        {
            _controller = new DiffController(new StubStateService(), new StubDiffService());
        }

        [TestMethod]
        public void TestValidateEmptyLeft()
        {
            var actual = _controller.GetResult("1");
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestValidateEmptyRight()
        {
            _controller.Set("1", Side.Left, new DiffModel { Data = new byte[] { 0, 1, 2 } });
            var actual = _controller.GetResult("1");
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestOk()
        {
            _controller.Set("1", Side.Left, new DiffModel { Data = new byte[] { 0, 1, 2 } });
            _controller.Set("1", Side.Right, new DiffModel { Data = new byte[] { 0, 1, 2 } });
            var actual = _controller.GetResult("1");
            Assert.IsInstanceOfType(actual, typeof(OkNegotiatedContentResult<DiffResult>));
        }
    }
}
