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
                await AddEntranceAsync("Norte");
                await AddEntranceAsync("Sur");
                await AddEntranceAsync("Oriental");
                await AddEntranceAsync("Occidental");

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
                    Entrance = _context.entrances.FirstOrDefault(),
                    //Entrance = new() { Id = 1 ,Description = "Norte" },
                    Date = new DateTime(0001, 01, 01, 00, 00, 00)
                };
                _context.tickets.Add(ticket);
            }
            
        }

        private async Task AddEntranceAsync(string description)
        {
            Entrance Entrance = new()
            {
                Description = description
            };
            _context.entrances.Add(Entrance);
        }
    }
}
