using System.ComponentModel.DataAnnotations;

namespace CineCartelera.Models
{
    public class Director
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre del director es obligatorio")]
        public string Nombre { get; set; }

      

    }
}
