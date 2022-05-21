using CineCartelera.Datos;
using CineCartelera.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineCartelera.Controllers
{

    [Authorize(Roles = WC.AdminRole)]
    public class DirectorController : Controller
    {

        private readonly ApplicationDbContext _db;

        public DirectorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Director> lista = _db.Director;
            return View(lista);
        }

        //Get
        public IActionResult Agregar()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(Director director)
        {
            if (ModelState.IsValid)
            {
                _db.Director.Add(director);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(director);


        }


        //Get Editar
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Director.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Director director)
        {
            if (ModelState.IsValid)
            {
                _db.Director.Update(director);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(director);


        }


        //Get Eliminar
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Director.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Director director)
        {
            if (director == null)
            {
                return NotFound();

            }
            _db.Director.Remove(director);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));


        }


    }

}
