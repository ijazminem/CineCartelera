using System.ComponentModel.DataAnnotations;

namespace CineCartelera.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Es obligatorio introducir un nombre de Género de películas")]
        public string NombreGenero { get; set; }

       

        [Required(ErrorMessage = "Es obligatorio introducir un número")]
        public int MostrarOrden { get; set; }

    }
}
