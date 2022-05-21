
using CineCartelera.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CineCartelera.Datos
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {

        }




        public DbSet<Genero> Genero { get; set; }
        public DbSet<Director>  Director { get; set; }

        public DbSet<Pelicula> Pelicula { get; set; }



    }
}
