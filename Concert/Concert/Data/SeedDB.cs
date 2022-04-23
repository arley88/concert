using Concert.Data.Entities;

namespace Concert.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckEntrancesAsync();
            await CheckTicketsAsinc();
        }

        private async Task CheckTicketsAsinc()
        {
            if (!_context.tickets.Any())
            {
                await AddTicketsAsync();
                await _context.SaveChangesAsync();
            };
        }
        private async Task CheckEntrancesAsync()
        {
            if (!_context.entrances.Any())
            {
                await AddEntranceAsync(1, "Norte");
                await AddEntranceAsync(2, "Sur");
                await AddEntranceAsync(3, "Oriental");
                await AddEntranceAsync(4, "Occidental");

                await _context.SaveChangesAsync();
            };
        }

        private async Task AddTicketsAsync()
        {
            for (int i = 1; i <= 5000; i++)
            {
                Ticket ticket = new()
                {
                    WasUsed = false,
                    Document = "",
                    Name = "Disponible",
                    Entrance = new() { Id = 0 ,Description = "" },
                    Date = new DateTime(0000, 00, 00, 00, 00, 00)
                };
                _context.tickets.Add(ticket);
            }
            
        }

        private async Task AddEntranceAsync(int id, string description)
        {
            Entrance Entrance = new()
            {
                Id = id,
                Description = description
            };
            _context.entrances.Add(Entrance);
        }
    }
}
