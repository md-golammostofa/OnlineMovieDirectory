using MovieDirectory.Models;
using MovieDirectory.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MovieDirectory.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        MovieDbContext db = new MovieDbContext();
        // GET: Genre
        [AllowAnonymous]
        public ActionResult Index(int id = 0)
        {
            MovieIndexVM vm = new MovieIndexVM
            {
                SelectedGenre = id == 0 ? null : db.Genres.First(x => x.GenreId == id),
                Genres = db.Genres.Include(s => s.Movies).ToList()
            };
            return View(vm);
        }
        [AllowAnonymous]
        public PartialViewResult MovieList(int id)
        {
            return PartialView("_MoviePartial", db.Movies.Where(x => x.GenreId == id).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Genre g)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(g);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(g);
        }
        public ActionResult Edit(int id)
        {
            var g = db.Genres.FirstOrDefault(x => x.GenreId == id);
            return View(g);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Genre g)
        {
            if (ModelState.IsValid)
            {
                db.Entry(g).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(g);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Genres.FirstOrDefault(x => x.GenreId == id));
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var g = db.Genres.FirstOrDefault(x => x.GenreId == id);
            db.Entry(g).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}