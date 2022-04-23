using Concert.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Concert.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboEntrancesAsync()
        {
            List<SelectListItem> list = await _context.entrances.Select(c => new SelectListItem
            {
                Text = c.Description,
                Value = c.Id.ToString()
            }).OrderBy(c => c.Text)
              .ToListAsync();
            list.Insert(0, new SelectListItem { Text = "[Seleccione un Entrada...]", Value = "0" });
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int EntranceId)
        {
            List<SelectListItem> list = await _context.tickets.Where(c => c.Entrance.Id == EntranceId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).OrderBy(c => c.Text)
              .ToListAsync();
            list.Insert(0, new SelectListItem { Text = "[Seleccione un Departamento/Estado...]", Value = "0" });
            return list;
        }

        public Task<IEnumerable<SelectListItem>> GetComboTicketsAsync(int EntranceId)
        {
            throw new NotImplementedException();
        }
    }
}
