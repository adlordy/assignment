using adlordy.Assignment.Services;
using adlordy.Assignment.Models;
using System.Linq;
using Xunit;
using System;
using adlordy.Assignment.Contracts;

namespace adlordy.Assignment.IntegrationTests
{
    public class DiffServiceTest : IClassFixture<HttpClientFixture>
    {
        private IDiffService _service;

        public DiffServiceTest(HttpClientFixture fixture)
        {
            _service = fixture.Service;
        }

        [Theory]
        [InlineData(null,new byte[0])]
        [InlineData(new byte[0],null)]
        public void TestNull(byte[] left, byte[] right)
        {
            Assert.Throws<AggregateException>(() => _service.GetDiff(left, right));
        }
        
        [Fact]
        public void TestEmpty()
        {
            var actual = _service.GetDiff(new byte[0], new byte[0]);
            Assert.Equal(DiffResultType.Equals, actual.DiffResultType);
            Assert.Null(actual.Diffs);
        }

        [Theory]
        [InlineData(new byte[] {1,1,1,1,1}, new byte[0])]
        [InlineData(new byte[0], new byte[] { 1, 1, 1, 1, 1 })]
        public void TestSizeDoNotMatch(byte[] left, byte[] right)
        {
            var actual = _service.GetDiff(left,right);
            Assert.Equal(DiffResultType.SizeDoNotMatch, actual.DiffResultType);
            Assert.Null(actual.Diffs);
        }

        [Fact]
        public void TestBeginningSingle()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 0, 2, 3, 4, 5 };
            var actual = _service.GetDiff(left, right);
            Assert.Equal(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.NotNull(actual.Diffs);
            Assert.Equal(1, actual.Diffs.Count());
            Assert.Equal(0, actual.Diffs.ElementAt(0).Offset);
            Assert.Equal(1, actual.Diffs.ElementAt(0).Length);
        }

        [Fact]
        public void TestBeginningMultiple()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 0, 0, 0, 4, 5 };
            var actual = _service.GetDiff(left, right);
            Assert.Equal(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.NotNull(actual.Diffs);
            Assert.Equal(1, actual.Diffs.Count());
            Assert.Equal(0, actual.Diffs.ElementAt(0).Offset);
            Assert.Equal(3, actual.Diffs.ElementAt(0).Length);
        }

        [Fact]
        public void TestMiddleSingle()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 1, 2, 0, 4, 5 };
            var actual = _service.GetDiff(left, right);
            Assert.Equal(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.NotNull(actual.Diffs);
            Assert.Equal(1, actual.Diffs.Count());
            Assert.Equal(2, actual.Diffs.ElementAt(0).Offset);
            Assert.Equal(1, actual.Diffs.ElementAt(0).Length);
        }

        [Fact]
        public void TestMiddleMultiple()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 1, 0, 0, 0, 5 };
            var actual = _service.GetDiff(left, right);
            Assert.Equal(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.NotNull(actual.Diffs);
            Assert.Equal(1, actual.Diffs.Count());
            Assert.Equal(1, actual.Diffs.ElementAt(0).Offset);
            Assert.Equal(3, actual.Diffs.ElementAt(0).Length);
        }

        [Fact]
        public void TestEndSingle()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 1, 2, 3, 4, 0 };
            var actual = _service.GetDiff(left, right);
            Assert.Equal(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.NotNull(actual.Diffs);
            Assert.Equal(1, actual.Diffs.Count());
            Assert.Equal(4, actual.Diffs.ElementAt(0).Offset);
            Assert.Equal(1, actual.Diffs.ElementAt(0).Length);
        }

        [Fact]
        public void TestEndMultiple()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 1, 2, 0, 0, 0 };
            var actual = _service.GetDiff(left, right);
            Assert.Equal(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.NotNull(actual.Diffs);
            Assert.Equal(1, actual.Diffs.Count());
            Assert.Equal(2, actual.Diffs.ElementAt(0).Offset);
            Assert.Equal(3, actual.Diffs.ElementAt(0).Length);
        }

        [Fact]
        public void TestMany()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 0, 0, 3, 4, 0 };
            var actual = _service.GetDiff(left, right);
            Assert.Equal(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.NotNull(actual.Diffs);
            Assert.Equal(2, actual.Diffs.Count());
            Assert.Equal(0, actual.Diffs.ElementAt(0).Offset);
            Assert.Equal(2, actual.Diffs.ElementAt(0).Length);
            Assert.Equal(4, actual.Diffs.ElementAt(1).Offset);
            Assert.Equal(1, actual.Diffs.ElementAt(1).Length);
        }

        [Fact]
        public void TestWhole()
        {
            var left = new byte[] { 1, 2, 3, 4, 5 };
            var right = new byte[] { 0, 0, 0, 0, 0 };
            var actual = _service.GetDiff(left, right);
            Assert.Equal(DiffResultType.ContentDoNotMatch, actual.DiffResultType);
            Assert.NotNull(actual.Diffs);
            Assert.Equal(1, actual.Diffs.Count());
            Assert.Equal(0, actual.Diffs.ElementAt(0).Offset);
            Assert.Equal(5, actual.Diffs.ElementAt(0).Length);
        }

    }
}
