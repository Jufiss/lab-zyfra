using Microsoft.EntityFrameworkCore;
using Server.Interfaces;
using Server.Models;

namespace Server.Repositories
{
    public class StateRepository : BaseRepository, IStateRepository
    {
        public StateRepository(Context context) : base(context)
        {
        }

        public IEnumerable<State> GetAllStates()
        {
            return db.State.ToList();
        }

        public State? GetStateById(int id) 
        {
            var state = db.State.FirstOrDefault(p => p.Id == id);
            return state;
        }

        public void Add(State state)
        {
            db.State.Add(state);
            Save();
        }

        public void UpdateState(int id, string value)
        {
            var state = GetStateById(id);
            if (state != null) 
            {
                state.Value = value;
                db.Entry(state).State = EntityState.Modified;
            }
            else
            {
                Add(state);
            }
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
