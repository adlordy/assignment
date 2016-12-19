using adlordy.Assignment.Contracts;
using adlordy.Assignment.Models;

namespace adlordy.Assignment.Tests.Stub
{
    public class StubStateService : IStateService
    {
        byte[] _left;
        byte[] _right;
        public byte[] Get(string id, Side side)
        {
            return side == Side.Left ? _left : _right;
        }

        public void Set(string id, Side side, byte[] data)
        {
            if (side == Side.Left)
                _left = data;
            else
                _right = data;
        }
    }
}
