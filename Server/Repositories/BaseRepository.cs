using Server.Models;

namespace Server.Repositories
{
    public class BaseRepository
    {
        public Context db;
        public BaseRepository(Context context)
        {
            db = context;
        }
    }
}
