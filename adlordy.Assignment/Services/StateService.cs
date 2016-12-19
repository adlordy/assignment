using System;
using adlordy.Assignment.Contracts;
using adlordy.Assignment.Models;
using System.Collections.Concurrent;

namespace adlordy.Assignment.Services
{
    public class StateService : IStateService
    {
        private class Key
        {
            public Key(string id, Side side)
            {
                Id = id;
                Side = side;
            }

            public string Id { get; }
            public Side Side { get; }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                var key = (Key)obj;
                return key.Id == Id && key.Side == Side;
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode() ^ Side.GetHashCode();
            }
        }

        private readonly ConcurrentDictionary<Key, byte[]> _state = new ConcurrentDictionary<Key, byte[]>();

        public byte[] Get(string id, Side side)
        {
            byte[] result;
            return _state.TryGetValue(new Key(id, side), out result) ? result : null;
        }

        public void Set(string id, Side side, byte[] data)
        {
            _state.AddOrUpdate(new Key(id, side), data, (key, original) => data);
        }
    }
}
