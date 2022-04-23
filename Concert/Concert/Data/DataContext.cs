using Concert.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Concert.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Entrance> entrances { get; set; }
        public DbSet<Ticket> tickets  { get; set; }
    }
}
   
