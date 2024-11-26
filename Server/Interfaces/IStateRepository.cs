using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Interfaces
{
    public interface IStateRepository
    {
        public IEnumerable<State> GetAllStates();

        public State? GetStateById(int id);

        public void Add(State state);

        public void UpdateState(int id, string value);
    }
}
