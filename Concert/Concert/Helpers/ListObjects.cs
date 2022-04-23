using Concert.Data;
using Concert.Data.Entities;

namespace Concert.Helpers
{
    public class ListObjects
    {
        private readonly DataContext _context;
        public ListObjects(DataContext context)
        {
            _context = context;
        }

        public ICollection<Ticket> tickets;
        public ICollection<Entrance> entrances;

        //public async ICollection<Entrance> GetListEntrances()
        //{
        //    return ;
        //}

    }
}
