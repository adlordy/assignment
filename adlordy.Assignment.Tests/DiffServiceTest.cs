using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adlordy.Assignment.Contracts;
using adlordy.Assignment.Services;
using adlordy.Assignment.Models;
using System.Linq;

namespace adlordy.Assignment.Tests
{
    [TestClass]
    public class DiffServiceTest
    {
        private DiffService _service;

        [TestInitialize]
        public void Init()
        {
            _service = new DiffService();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLeftNull()
        {
            _service.GetDiff(null, new byte[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRightNull()
        {
            _service.GetDiff(new byte[0], null);
        }

        [TestMethod]
        public void TestEmpty()
        {
            var actual = _service.GetDiff(new byte[0], new byte[0]);
            Assert.AreEqual(DiffResultType.Equals, actual.DiffResultType);
            Assert.IsNull(actual.Diffs);
        }

        [TestMethod]
        public void TestLeftIsLonger()
        {
            var actual = _service.GetDiff(new byte[5], new byte[0]);
            Assert.AreEqual(DiffResultType.SizeDoNotMatch, actual.DiffResultType);
            Assert.IsNull(actual.Diffs);
        }

        [TestMethod]
        public void TestRightIsLonger()
        {
            var actual = _service.GetDiff(new byte[0], new byte[5]);
            Assert.AreEqual(DiffResultType.SizeDoNotMatch, actual.DiffResultType);
            Assert.IsNull(actual.Diffs);
        }

        [TestMethod]
        public void TestBeginningSingle()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 0, 2, 3, 4, 5 };
            var actual = _service.GetDiff(left, right);
            Assert.AreEqual(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.IsNotNull(actual.Diffs);
            Assert.AreEqual(1, actual.Diffs.Count(), "Count is wrong");
            Assert.AreEqual(0, actual.Diffs.ElementAt(0).Offset, "Offset is wrong");
            Assert.AreEqual(1, actual.Diffs.ElementAt(0).Length, "Length is wrong");
        }

        [TestMethod]
        public void TestBeginningMultiple()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 0, 0, 0, 4, 5 };
            var actual = _service.GetDiff(left, right);
            Assert.AreEqual(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.IsNotNull(actual.Diffs);
            Assert.AreEqual(1, actual.Diffs.Count(), "Count is wrong");
            Assert.AreEqual(0, actual.Diffs.ElementAt(0).Offset, "Offset is wrong");
            Assert.AreEqual(3, actual.Diffs.ElementAt(0).Length, "Length is wrong");
        }

        [TestMethod]
        public void TestMiddleSingle()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 1, 2, 0, 4, 5 };
            var actual = _service.GetDiff(left, right);
            Assert.AreEqual(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.IsNotNull(actual.Diffs);
            Assert.AreEqual(1, actual.Diffs.Count(), "Count is wrong");
            Assert.AreEqual(2, actual.Diffs.ElementAt(0).Offset, "Offset is wrong");
            Assert.AreEqual(1, actual.Diffs.ElementAt(0).Length, "Length is wrong");
        }

        [TestMethod]
        public void TestMiddleMultiple()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 1, 0, 0, 0, 5 };
            var actual = _service.GetDiff(left, right);
            Assert.AreEqual(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.IsNotNull(actual.Diffs);
            Assert.AreEqual(1, actual.Diffs.Count(), "Count is wrong");
            Assert.AreEqual(1, actual.Diffs.ElementAt(0).Offset, "Offset is wrong");
            Assert.AreEqual(3, actual.Diffs.ElementAt(0).Length, "Length is wrong");
        }

        [TestMethod]
        public void TestEndSingle()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 1, 2, 3, 4, 0 };
            var actual = _service.GetDiff(left, right);
            Assert.AreEqual(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.IsNotNull(actual.Diffs);
            Assert.AreEqual(1, actual.Diffs.Count(), "Count is wrong");
            Assert.AreEqual(4, actual.Diffs.ElementAt(0).Offset, "Offset is wrong");
            Assert.AreEqual(1, actual.Diffs.ElementAt(0).Length, "Length is wrong");
        }

        [TestMethod]
        public void TestEndMultiple()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 1, 2, 0, 0, 0 };
            var actual = _service.GetDiff(left, right);
            Assert.AreEqual(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.IsNotNull(actual.Diffs);
            Assert.AreEqual(1, actual.Diffs.Count(), "Count is wrong");
            Assert.AreEqual(2, actual.Diffs.ElementAt(0).Offset, "Offset is wrong");
            Assert.AreEqual(3, actual.Diffs.ElementAt(0).Length, "Length is wrong");
        }

        [TestMethod]
        public void TestMany()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 0, 0, 3, 4, 0 };
            var actual = _service.GetDiff(left, right);
            Assert.AreEqual(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.IsNotNull(actual.Diffs);
            Assert.AreEqual(2, actual.Diffs.Count(), "Count is wrong");
            Assert.AreEqual(0, actual.Diffs.ElementAt(0).Offset, "Offset 0 is wrong");
            Assert.AreEqual(2, actual.Diffs.ElementAt(0).Length, "Length 0 is wrong");
            Assert.AreEqual(4, actual.Diffs.ElementAt(1).Offset, "Offset 1 is wrong");
            Assert.AreEqual(1, actual.Diffs.ElementAt(1).Length, "Length 1 is wrong");
        }

        [TestMethod]
        public void TestWhole()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 0, 0, 0, 0, 0 };
            var actual = _service.GetDiff(left, right);
            Assert.AreEqual(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.IsNotNull(actual.Diffs);
            Assert.AreEqual(1, actual.Diffs.Count(), "Count is wrong");
            Assert.AreEqual(0, actual.Diffs.ElementAt(0).Offset, "Offset is wrong");
            Assert.AreEqual(5, actual.Diffs.ElementAt(0).Length, "Length is wrong");
        }

    }
}
