using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineCartelera.Models.ViewModels
{
    public class PeliculaVM
    {
        public Pelicula Pelicula { get; set; }

        public IEnumerable<SelectListItem>? GeneroLista { get; set; }

        public IEnumerable<SelectListItem>? DirectorLista { get; set; }

    }
}
