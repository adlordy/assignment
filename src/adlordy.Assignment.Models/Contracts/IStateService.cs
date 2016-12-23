using adlordy.Assignment.Models;

namespace adlordy.Assignment.Contracts
{
    public interface IStateService
    {
        void Set(string id, Side side, byte[] data);
        byte[] Get(string id, Side side);
    }
}