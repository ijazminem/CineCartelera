using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineCartelera.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre de la película es requerido")]
        public string NombrePelicula { get; set; }

        [Required(ErrorMessage ="Sinopsis de la película es requerida")]
        public string Sinopsis { get; set; }

        public string? ImagenUrl { get; set; }


        public int GeneroId { get; set; }

        [ForeignKey("GeneroId")]
        public  virtual Genero? Genero { get; set; }

        public int DirectorId { get; set; }

        [ForeignKey("DirectorId")]
        public virtual Director? Director { get; set; }

    }
}
