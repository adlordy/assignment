using adlordy.Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace adlordy.Assignment.IntegrationTests
{
    [Collection("Client Tests")]
    public class HttpClientTests : IClassFixture<HttpClientFixture>
    {
        private readonly DiffClient _client;

        public HttpClientTests(HttpClientFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task TestEmpty_WhenNeitherLeftNorRightSet()
        {
            var result = await _client.GetAsync("1");
            Assert.Null(result);
        }

        [Theory]
        [InlineData("2", new byte[] { 0 }, null)]
        [InlineData("3", null, new byte[] { 0 })]
        public async Task TestEmpty_WhenLeftOrRightNotSet(string id, byte[] left, byte[] right)
        {
            if (left != null)
                await _client.PutLeftAsync(id, left);
            if (right != null)
                await _client.PutRightAsync(id, right);
            var result = await _client.GetAsync(id);
            Assert.Null(result);
        }

        //[Fact]
        //public void TestBadRequest_WhenDataIsNull()
        //{
        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        try
        //        {
        //            _client.PutLeftAsync("4", null).Wait();
        //        }
        //        catch (AggregateException ex)
        //        {
        //            throw ex.Flatten().InnerExceptions.First();
        //        }
        //    });
        //}

        [Theory]
        [InlineData("5", 2)]
        [InlineData("6", 10)]
        public async Task TestOk_MultipleLeft(string id, byte count)
        {
            for (byte i = 0; i < count; i++)
            {
                await _client.PutLeftAsync(id, new byte[] { i });
            }
        }

        [Theory]
        [InlineData("7", 5)]
        [InlineData("8", 10)]
        public async Task TestOk_ParallelLeft(string idPrefix, int count)
        {
            var putTasks = Enumerable.Range(0, count).SelectMany(i => GetPutTasks(idPrefix, i, count)).ToArray();
            await Task.WhenAll(putTasks);
            var getTasks = Enumerable.Range(0, count).Select(i => _client.GetAsync(idPrefix + i));
            var results = await Task.WhenAll(getTasks);
            foreach (var result in results)
            {
                Assert.NotNull(result);
                Assert.Equal(DiffResultType.Equals, result.DiffResultType);
            }
        }

        private IEnumerable<Task> GetPutTasks(string idPrefix, int i, int count)
        {
            var data = Enumerable.Repeat(i, count)
                .Select(b => (byte)b).ToArray();

            yield return _client.PutLeftAsync(idPrefix + i, data);
            yield return _client.PutRightAsync(idPrefix + i, data);
        }
    }
}
