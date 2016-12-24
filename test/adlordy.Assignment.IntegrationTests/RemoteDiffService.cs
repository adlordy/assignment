using adlordy.Assignment.Contracts;
using adlordy.Assignment.Models;
using System;
using System.Linq;
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
            try
            {
                return Task.Run(async () =>
                {
                    await _client.PutLeftAsync(id, left);
                    await _client.PutRightAsync(id, right);
                    return await _client.GetAsync(id);
                }).Result;
            } catch(AggregateException ex)
            {
                throw ex.Flatten().InnerExceptions.First();
            }
        }
    }
}