using CineCartelera.Datos;
using CineCartelera.Models;
using CineCartelera.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace CineCartelera.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class PeliculaController : Controller
    {

        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public PeliculaController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Pelicula> lista = _db.Pelicula.Include(c=>c.Genero)
                                                      .Include(t => t.Director);
            return View(lista);
        }


        //Metodo get

        public IActionResult Upsert(int? Id)
        {
           

            PeliculaVM peliculaVM = new PeliculaVM()
            {
                Pelicula = new Pelicula(),
                GeneroLista = _db.Genero.Select(c=> new SelectListItem
                {
                    Text = c.NombreGenero,
                    Value = c.Id.ToString()
                }),
                DirectorLista = _db.Director.Select(c=>new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                })

            };


            
            if(Id == null)

            {
                //Crear película nueva
                return View(peliculaVM);
            }
            else
            {
                peliculaVM.Pelicula = _db.Pelicula.Find(Id);
                if (peliculaVM.Pelicula == null)
                {
                    return NotFound();
                }
                return View(peliculaVM);
            }


              
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PeliculaVM peliculaVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (peliculaVM.Pelicula.Id == 0)
                {
                    //Crear
                    string upload = webRootPath + WC.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    peliculaVM.Pelicula.ImagenUrl = fileName + extension;
                    _db.Pelicula.Add(peliculaVM.Pelicula);
                }
                else
                {
                    //Actualizar
                    var objPelicula = _db.Pelicula.AsNoTracking().FirstOrDefault(p => p.Id == peliculaVM.Pelicula.Id);
                    if(files.Count >0)
                    {
                        string upload = webRootPath + WC.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //borrando imagen anterior
                        var anteriorFile = Path.Combine(upload, objPelicula.ImagenUrl);
                        if(System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        // imagen anterior eliminada

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        peliculaVM.Pelicula.ImagenUrl = fileName + extension;
                    }
                    else
                    {
                        peliculaVM.Pelicula.ImagenUrl = objPelicula.ImagenUrl;
                    }
                    _db.Pelicula.Update(peliculaVM.Pelicula);

                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            peliculaVM.GeneroLista = _db.Genero.Select(c => new SelectListItem
            {
                Text = c.NombreGenero,
                Value = c.Id.ToString()
            });
            peliculaVM.DirectorLista = _db.Director.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            });


            return View(peliculaVM);



        }


        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();



            Pelicula pelicula = _db.Pelicula.Include(c => c.Genero)
                                           .Include(t => t.Director)
                                            .FirstOrDefault(p => p.Id == Id);

            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Pelicula pelicula)
        {
            if(pelicula == null)
            {
                return NotFound();
            }
            //Eliminar imagen del directorio en la base de datos
            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;
           
            //borrando imagen anterior
            var anteriorFile = Path.Combine(upload, pelicula.ImagenUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }


            _db.Pelicula.Remove(pelicula);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));


        }


    }
}
