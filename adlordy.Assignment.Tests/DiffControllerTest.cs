using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adlordy.Assignment.Controllers.V1;
using adlordy.Assignment.Tests.Dummy;
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
            _controller = new DiffController(new DummyDiffService());
        }

        [TestMethod]
        public void TestValidateEmptyLeft()
        {
            var actual = _controller.GetResult();
            Assert.IsInstanceOfType(actual, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void TestValidateEmptyRight()
        {
            _controller.SetLeft(new DiffModel { Data = new byte[] { 0, 1, 2 } });
            var actual = _controller.GetResult();
            Assert.IsInstanceOfType(actual, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void TestOk()
        {
            _controller.SetLeft(new DiffModel { Data = new byte[] { 0, 1, 2 } });
            _controller.SetRight(new DiffModel { Data = new byte[] { 0, 1, 2 } });
            var actual = _controller.GetResult();
            Assert.IsInstanceOfType(actual, typeof(OkNegotiatedContentResult<DiffResult>));
        }
    }
}
