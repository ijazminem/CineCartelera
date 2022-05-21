using CineCartelera.Datos;
using CineCartelera.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineCartelera.Controllers
{

    [Authorize(Roles = WC.AdminRole)]
    public class GeneroController : Controller
    {

        private readonly ApplicationDbContext _db;

        public GeneroController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Genero> lista = _db.Genero;
            return View(lista);
        }

        //Get
        public IActionResult Agregar()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(Genero genero)
        {
            if(ModelState.IsValid)
            {
                _db.Genero.Add(genero);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(genero);

           
        }


        //Get Editar
        public IActionResult Editar(int? Id)
        {
            if(Id== null || Id==0)
            {
                return NotFound();
            }
            var obj = _db.Genero.Find(Id);
            if(obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Genero genero)
        {
            if (ModelState.IsValid)
            {
                _db.Genero.Update(genero);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(genero);


        }


        //Get Eliminar
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Genero.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Genero genero)
        {
            if (genero == null)
            {
                return NotFound();

            }
            _db.Genero.Remove(genero);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));


        }


    }

}
