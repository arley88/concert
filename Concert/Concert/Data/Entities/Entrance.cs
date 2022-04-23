using System.ComponentModel.DataAnnotations;

namespace Concert.Data.Entities
{
    public class Entrance
    {

        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; }
        public ICollection<Ticket> tickets { get; set; }
    }
}
