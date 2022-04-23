using System.ComponentModel.DataAnnotations;

namespace Concert.Data.Entities
{
    public class Ticket
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

        public Entrance Entrance { get; set; }

        public DateTime Date { get; set; }
    }
}

