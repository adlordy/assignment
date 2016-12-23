using adlordy.Assignment.Contracts;
using adlordy.Assignment.Models;
using System.Threading;
using System.Threading.Tasks;

namespace adlordy.Assignment.IntegrationTests
{
    internal class RemoteDiffService : IDiffService
    {
        private DiffClient _client;
        private int _seed;

        public RemoteDiffService(DiffClient client, int seed)
        {
            _client = client;
            _seed = seed;
        }

        public DiffResult GetDiff(byte[] left, byte[] right)
        {
            var id = Interlocked.Increment(ref _seed).ToString();
            return Task.Run(async () =>
            {
                await _client.PutLeftAsync(id, left);
                await _client.PutRightAsync(id, right);
                return await _client.GetAsync(id);
            }).Result;
        }
    }
}