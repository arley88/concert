using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Concert.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public bool WasUsed { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        public DateTime Date { get; set; }


        [Display(Name = "Entrada")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una Entrada.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int EntranceId { get; set; }

        public IEnumerable<SelectListItem> Entrances { get; set; }
    }
        
}
