using Concert.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Concert.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboEntrancesAsync();
        Task<IEnumerable<SelectListItem>> GetComboTicketsAsync(int EntranceId );
    }
}
