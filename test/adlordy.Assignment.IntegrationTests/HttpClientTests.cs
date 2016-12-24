using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace adlordy.Assignment.IntegrationTests
{
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
        [InlineData("2",new byte[] {0},null)]
        [InlineData("3",null, new byte[] {0})]
        public async Task TestEmpty_WhenLeftOrRightNotSet(string id, byte[] left, byte[] right)
        {
            if (left != null)
                await _client.PutLeftAsync(id, left);
            if (right != null)
                await _client.PutRightAsync(id, right);
            var result = await _client.GetAsync(id);
            Assert.Null(result);
        }

        [Fact]
        public void TestBadRequest_WhenDataIsNull()
        {
            Assert.Throws<ArgumentException>(() => {
                try
                {
                    _client.PutLeftAsync("4", null).Wait();
                } catch(AggregateException ex)
                {
                    throw ex.Flatten().InnerExceptions.First();
                }
            });
        }
    }
}
